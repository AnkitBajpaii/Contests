using DemoApp.Business;
using System;
using System.Collections.Generic;

namespace DemoApp
{
    public class SplitWiseApp
    {
        static SplitWiseApp instance;
        private SplitWiseApp()
        {

        }

        public static SplitWiseApp Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SplitWiseApp();
                }

                return instance;
            }
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Spliwise");
            string endSession;
            do
            {
                Console.WriteLine("Session started for new user.");

                Console.WriteLine("Enter your User Name");
                string primaryUserName = Console.ReadLine();

                Console.WriteLine("Enter your User Email");
                string primaryUserEmail = Console.ReadLine();

                User primaryUser = new User(new Person() { Name = primaryUserName, Email = primaryUserEmail });
                List<User> sharedWith = new List<User>() { };
                string continueWith;
                do
                {
                    Console.WriteLine("Do you want to Add Expense ?(1/0)");
                    string addExpense = Console.ReadLine();
                    if (addExpense == "1")
                    {
                        Console.WriteLine("Enter users to share with");
                        string addMore;
                        do
                        {
                            Console.WriteLine("Enter User Name");
                            string userName = Console.ReadLine();
                            Console.WriteLine("Enter User Email");
                            string userEmail = Console.ReadLine();
                            sharedWith.Add(new User(new Person() { Name = userName, Email = userEmail }));

                            Console.WriteLine("Want to Add More users to share with?(1/0)");

                            addMore = Console.ReadLine();

                        } while (addMore == "1");


                        Console.WriteLine("Enter Expense description");
                        string desc = Console.ReadLine();

                        Console.WriteLine("Enter Expense Amount");
                        double amount = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Enter Currency (INR/USD)");
                        Currency currency = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine());

                        Console.WriteLine("Enter Expense strategy (Equal/Percentage)");
                        string strategy = Console.ReadLine();
                        ISplitStrategy splitStrategy = strategy == "Equal" ? new EqualSplitStrategy() : null;

                        primaryUser.AddExpense(currency, desc, amount, primaryUser, splitStrategy, sharedWith.ToArray());

                        Console.WriteLine("Expense added successfully.");
                    }

                    Console.WriteLine("Do you want to continue?(1/0)");
                    continueWith = Console.ReadLine();

                } while (continueWith == "1");

                Console.WriteLine("Expense Report");
                Console.WriteLine();
                primaryUser.ShowExpense();
                Console.WriteLine();

                Console.WriteLine("Do you want to continue or End Session (1/0)");
                endSession = Console.ReadLine();
            } while (endSession == "1");

            Console.WriteLine("Thank you! Press any key to exit");
            Console.ReadKey();
        }
    }
}
