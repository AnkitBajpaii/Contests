using System.Collections.Generic;

namespace DemoApp.Business
{
    public class Expense
    {
        public int Id { get; }
        public double Amount { get; set; }

        public Currency Currency { get; set; }
        
        public string Description { get; set; }

        public User PaidBy { get; }

        public List<User> BelongsTo { get; }

        public ISplitStrategy SplitStrategy { get; }

        public Expense(List<User> belongsTo, User paidBy, ISplitStrategy splitStrategy)
        {
            PaidBy = paidBy;
            BelongsTo = belongsTo;
            SplitStrategy = splitStrategy;

            Id = ExpenseIdCounter.Instance.CurrentCount + 1;
            ExpenseIdCounter.Instance.CurrentCount++;
        }

        public void AddUserToShareWith(User userToShareWith)
        {
            BelongsTo.Add(userToShareWith);

            this.SplitBill();
        }

        public void AddUsersToShareWith(List<User> usersToShareWith)
        {
            BelongsTo.AddRange(usersToShareWith);
        }

        public void SplitBill()
        {
            SplitStrategy.Split(this.Amount, BelongsTo);
        }

    }
}
