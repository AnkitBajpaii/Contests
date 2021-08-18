using System.Collections.Generic;

namespace DemoApp.Business
{
    public interface ISplitStrategy
    {
        void Split(double amount, List<User> users);
        string GetStrategyName();
    }
}
