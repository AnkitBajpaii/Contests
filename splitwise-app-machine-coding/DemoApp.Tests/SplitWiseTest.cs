using DemoApp.Business;
using Moq;
using NUnit.Framework;

namespace DemoApp.Tests
{
    public class SplitWiseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyExpenseForEqualSplit()
        {
            User u1 = new User(new Person() { Name ="A", Email="a@gmail.com"});
            User u2 = new User(new Person() { Name = "B", Email = "b@gmail.com" });
            User u3 = new User(new Person() { Name = "C", Email = "c@gmail.com" });

            u1.AddExpense(Currency.INR, "Dinner", 12000, u1, new EqualSplitStrategy(), u2, u3);

            Assert.That(u1.Owes, Is.EqualTo(4000));
            Assert.That(u2.Owes, Is.EqualTo(4000));
            Assert.That(u3.Owes, Is.EqualTo(4000));
        }
    }
}