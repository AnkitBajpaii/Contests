namespace DemoApp.Business
{
    public class ExpenseIdCounter
    {
        static ExpenseIdCounter instance;
        private ExpenseIdCounter()
        {

        }

        public int CurrentCount { get; set; }

        public static ExpenseIdCounter Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ExpenseIdCounter();
                }

                return instance;
            }
        }
    }
}
