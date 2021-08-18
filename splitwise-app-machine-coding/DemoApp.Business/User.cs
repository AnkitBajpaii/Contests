using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Business
{
    public class User
    {
        public Person Person { get; }

        public int Id { get; set; }
        public Dictionary<int, Expense> Expenses { get; }

        public double Owes { get; set; }

        public User(Person person)
        {
            Person = person;
            Expenses = new Dictionary<int, Expense>();
        }

        public int AddExpense(Currency currency, string desc, double amount, User paidBy, ISplitStrategy splitStrategy, params User[] sharedWith)
        {
            List<User> belongsTo = new List<User>();
            belongsTo.Add(paidBy);
            belongsTo.AddRange(sharedWith);

            Expense expense = new Expense(belongsTo, paidBy, splitStrategy) { Currency = currency, Description = desc, Amount = amount };
            
            expense.SplitBill();

            Expenses.Add(expense.Id, expense);

            return expense.Id;
        }

        public void AddUserToExpense(int expenseId, User user)
        {
            Expense expense = Expenses[expenseId];

            expense.AddUserToShareWith(user);
        }

        public void ShowExpense()
        {
            foreach (var entry in Expenses)
            {
                Console.WriteLine($"Expense Id: {entry.Key}");

                Expense expense = entry.Value;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Description: {expense.Description}");
                sb.AppendLine($"Amount: {expense.Amount}");
                sb.AppendLine($"Paid By: {expense.PaidBy.Person.Name}");
                
                List<string> sharedWith = expense.BelongsTo.Select(x => x.Person.Name).ToList();
                
                string str = string.Join(",", sharedWith);
                sb.AppendLine($"Shared With: {str}");

                sb.AppendLine($"Split Type: {expense.SplitStrategy.GetStrategyName()}");

                foreach (var user in expense.BelongsTo)
                {
                    sb.AppendLine($"{user.Person.Name} Owes: {user.Owes}");
                }

                Console.WriteLine(sb.ToString());
                Console.WriteLine();
                Console.WriteLine("**********************************************************");
                Console.WriteLine();
            }
        }
    }
}
