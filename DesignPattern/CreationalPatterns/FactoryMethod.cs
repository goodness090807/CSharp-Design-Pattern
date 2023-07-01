namespace DesignPattern.CreationalPatterns
{
    /// <summary>
    /// -----------------
    /// 工廠方法模式
    /// -----------------
    /// </summary>
    public class FactoryMethod
    {
        public static void Run()
        {
            ClientMethod(new ConcreteCreator1());
            ClientMethod(new ConcreteCreator2());
        }

        public static void ClientMethod(Creator creator)
        {
            Console.WriteLine(creator.SomeOperation());
        }

        public abstract class Creator
        {
            public abstract IProduct FactoryMethod();

            public string SomeOperation()
            {
                var product = FactoryMethod();

                return product.Operation();
            }
        }

        public class ConcreteCreator1 : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ConcreteProduct1();
            }
        }

        public class ConcreteCreator2 : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ConcreteProduct2();
            }
        }

        public interface IProduct
        {
            string Operation();
        }

        public class ConcreteProduct1 : IProduct
        {
            public string Operation()
            {
                return "調用了ConcreteProduct1";
            }
        }

        public class ConcreteProduct2 : IProduct
        {
            public string Operation()
            {
                return "調用了ConcreteProduct2";
            }
        }
    }
}
