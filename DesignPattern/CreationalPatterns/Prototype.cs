namespace DesignPattern.CreationalPatterns
{
    /// <summary>
    /// 原型模式
    /// 
    /// 大致上的意思是將所有的屬性或是方法複製到新的物件上
    /// 在C#中有提供MemberwiseClone這個淺複製的方法，可以複製相關屬性
    /// </summary>
    public class Prototype
    {
        public static void Run()
        {
            var person = new Person();
            person.Name = "Test";
            person.Age = 30;
            person.BirthDate = new DateTime(1999, 1, 1);
            person.IdInfo = new IdInfo(1);

            // 這邊複製出來的值，在變更前都是一樣的
            var person2 = person.ShallowCopy();
            var person3 = person.DeepCopy();

            Console.WriteLine("Original values of p1, p2, p3:");
            Console.WriteLine("   person instance values: ");
            DisplayValues(person);
            Console.WriteLine("   person2 instance values:");
            DisplayValues(person2);
            Console.WriteLine("   person3 instance values:");
            DisplayValues(person3);

            // 變更後就會有所差異
            person.Name = "Frank";
            person.Age = 32;
            person.BirthDate = new DateTime(2000, 1, 1);
            person.IdInfo.IdNumber = 100;

            Console.WriteLine("   person instance values: ");
            DisplayValues(person);
            Console.WriteLine("   person2 instance values (參考記憶體位址的會改變像是IdInfo.IdNumber):");
            DisplayValues(person2);
            Console.WriteLine("   person3 instance values (深層複製是連參考記憶體位址的都複製新的一份出來):");
            DisplayValues(person3);
        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.Name, p.Age, p.BirthDate);
            Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
        }

        public class Person
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            public DateTime BirthDate { get; set; }
            public IdInfo IdInfo { get; set; } = new IdInfo();

            /// <summary>
            /// 淺層複製
            /// 這個會有一個問題，就是如果是使用同一個記憶體位址，像是List或是一個Object，
            /// 當參考的實體改變時，複製出來的實體資料也會跟著改變
            /// </summary>
            public Person ShallowCopy()
            {
                return (Person)this.MemberwiseClone();
            }

            public Person DeepCopy()
            {
                var copy = (Person)this.MemberwiseClone();
                copy.IdInfo = new IdInfo(IdInfo.IdNumber);
                return copy;
            }
        }

        public class IdInfo
        {
            public int IdNumber;

            public IdInfo()
            {
            }

            public IdInfo(int idNumber)
            {
                IdNumber = idNumber;
            }
        }
    }
}
