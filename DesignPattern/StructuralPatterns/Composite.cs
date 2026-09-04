namespace DesignPattern.StructuralPatterns
{
    /// <summary>
    /// -----------------
    /// 組合設計模式
    /// -----------------
    /// </summary>
    public class Composite
    {
        public static void Run()
        {
            // 簡單的組合設計模式
            var rootCompany = new SampleComposite.Company("主要集團", 1000000);
            var subCompany1 = new SampleComposite.Company("子公司 1", 500000);
            var subCompany2 = new SampleComposite.Company("子公司 2", 400000);

            var employee1 = new SampleComposite.Employee("Employee 1", 100000);
            var employee2 = new SampleComposite.Employee("Employee 2", 90000);
            var employee3 = new SampleComposite.Employee("Employee 3", 80000);
            var employee4 = new SampleComposite.Employee("Employee 4", 70000);

            rootCompany.Add(subCompany1);
            rootCompany.Add(subCompany2);

            subCompany1.Add(employee1);
            subCompany1.Add(employee2);

            subCompany2.Add(employee3);
            subCompany2.Add(employee4);

            // 顯示公司組織結構
            rootCompany.Display(0);

            // 抽象的組合設計模式
            var leaf = new AbstractComposite.Leaf();
            Console.WriteLine(leaf.Operation());

            var tree = new AbstractComposite.Composit();
        }

        public class SampleComposite
        {
            public interface IComponent
            {
                string Name { get; }
                decimal Salary { get; set; }
                void Display(int depth);
            }

            /// <summary>
            /// Employee 在這邊是葉子是尾端
            /// 所以沒有實作Add和Remove的方法
            /// </summary>
            public class Employee : IComponent
            {
                public string Name { get; }
                public decimal Salary { get; set;}

                public Employee(string name, decimal salary)
                {
                    Name = name;
                    Salary = salary;
                }

                public void Display(int depth)
                {
                    Console.WriteLine(new string('-', depth) + Name + ": $" + Salary);
                }
            }

            public class Company : IComponent
            {
                /// <summary>
                /// 在Composite中，會有一個
                /// </summary>
                private readonly List<IComponent> _components = new List<IComponent>();

                public string Name { get; }
                public decimal Salary { get; set; }

                public Company(string name, decimal salary)
                {
                    Name = name;
                    Salary = salary;
                }

                public void Add(IComponent component)
                {
                    _components.Add(component);
                }

                public void Remove(IComponent component)
                {
                    _components.Remove(component);
                }

                public void Display(int depth)
                {
                    Console.WriteLine(new string('-', depth) + "Company: " + Name + ", Salary: $" + Salary);

                    foreach (var component in _components)
                    {
                        component.Display(depth + 2);
                    }
                }
            }
        }

        public class AbstractComposite
        {
            public abstract class Component
            {
                public abstract string Operation();

                /// <summary>
                /// 這邊的Add可以實作或不實作，因為葉子節點是不會有這個方法的
                /// </summary>
                public virtual void Add(Component component)
                {
                    throw new NotImplementedException();
                }

                /// <summary>
                /// 這邊的Remove可以實作或不實作，因為葉子節點是不會有這個方法的
                /// </summary>
                public virtual void Remove(Component component)
                {
                    throw new NotImplementedException();
                }
                public virtual bool IsComposite()
                {
                    return true;
                }
            }

            public class Leaf : Component
            {
                public override string Operation()
                {
                    return "Leaf";
                }

                public override bool IsComposite()
                {
                    return false;
                }
            }

            public class Composit : Component
            {
                protected List<Component> _components = new List<Component>();

                public override void Add(Component component)
                {
                    _components.Add(component);
                }

                public override void Remove(Component component)
                {
                    _components.Remove(component);
                }

                public override string Operation()
                {
                    int i = 0;
                    string result = "Branch(";

                    foreach (Component component in _components)
                    {
                        result += component.Operation();
                        if (i != this._components.Count - 1)
                        {
                            result += "+";
                        }
                        i++;
                    }

                    return result + ")";
                }
            }
        }
    }
}
