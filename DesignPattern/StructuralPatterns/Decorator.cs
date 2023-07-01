namespace DesignPattern.StructuralPatterns
{
    /// <summary>
    /// 裝飾器模式
    /// </summary>
    public class Decorator
    {
        public static void Run()
        {
            // 基本的元件呼叫
            var simple = new ConcreteComponent();
            ClientMethod(simple);

            // 這邊是加上裝飾器的呼叫方式
            var decorator1 = new ConcreteDecoratorB(simple);
            var decorator2 = new ConcreteDecoratorB(decorator1);

            ClientMethod(decorator2);

            // 也可以像這樣一直套同一層
            var decorator3 = new ConcreteDecoratorA(decorator1);
            ClientMethod(decorator3);
        }

        public static void ClientMethod(Component component)
        {
            Console.WriteLine(component.Operation());
        }

        /// <summary>
        /// 這個可以改為interface + virtual的寫法
        /// </summary>
        public abstract class Component
        {
            public abstract string Operation();
        }

        /// <summary>
        /// 這個是基本的實作而已
        /// </summary>
        public class ConcreteComponent : Component
        {
            public override string Operation()
            {
                return "ConcreteComponent";
            }
        }

        /// <summary>
        /// 這邊為主要的裝飾器，如果要包裝，需要繼承這個
        /// </summary>
        public class ComponentDecorator : Component
        {
            protected Component _component;

            public ComponentDecorator(Component component)
            {
                _component = component;
            }

            public override string Operation()
            {
                return _component.Operation();
            }
        }

        public class ConcreteDecoratorA : ComponentDecorator
        {
            public ConcreteDecoratorA(Component component) : base(component)
            {
            }

            public override string Operation()
            {
                return $"ConcreteDecoratorA({base.Operation()})";
            }
        }

        public class ConcreteDecoratorB : ComponentDecorator
        {
            public ConcreteDecoratorB(Component component) : base(component)
            {
            }

            public override string Operation()
            {
                return $"ConcreteDecoratorB({base.Operation()})";
            }
        }
    }
}
