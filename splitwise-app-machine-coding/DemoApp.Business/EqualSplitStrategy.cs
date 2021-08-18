using System.Collections.Generic;

namespace DemoApp.Business
{
    public class EqualSplitStrategy : ISplitStrategy
    {
        public string GetStrategyName()
        {
            return "Equal Split";
        }

        public void Split(double amount, List<User> users)
        {
            double split = (double)amount / users.Count;
            foreach (var user in users)
            {
                user.Owes = split;
            }
        }
    }
}
