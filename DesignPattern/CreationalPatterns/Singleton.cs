namespace DesignPattern.CreationalPatterns
{
    /// <summary>
    /// 單例設計模式
    /// </summary>
    public class Singleton
    {
        public static void Run()
        {
            // 基礎單例模式
            var s1 = BasicSingleton.GetInstance();
            var s2 = BasicSingleton.GetInstance();

            Console.WriteLine("基礎單例模式");
            Console.WriteLine("------------------------------");
            // 能發現在實體1設定資料，實體2是能取到的
            s1.SetName("test");
            Console.WriteLine(s2.GetName());

            // 如果判斷實體是否相等，能得到相等的結果
            Console.WriteLine(s1 == s2);

            Console.WriteLine("執行續安全單例模式");
            Console.WriteLine("------------------------------");
            // 執行續安全單例模式
            // 能發現Print出來的結果會是一樣的，可能是Test或是New One，看誰先執行到
            var t1 = new Thread(() =>
            {
                var safeSingleton = ThreadSafeSingleton.GetInstance("Test");
                Console.WriteLine(safeSingleton.Name);
            });
            var t2 = new Thread(() =>
            {
                var safeSingleton = ThreadSafeSingleton.GetInstance("New One");
                Console.WriteLine(safeSingleton.Name);
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        /// <summary>
        /// 基礎單例模式
        /// 
        /// 這個算是基本的實現，但不是很好的寫法
        /// 因為在多執行緒上會遇到問題
        /// </summary>
        public class BasicSingleton
        {
            private BasicSingleton(){}

            private static BasicSingleton? _instance;
            private string _name = string.Empty;

            public static BasicSingleton GetInstance()
            {
                if (_instance == null)
                    _instance = new BasicSingleton();

                return _instance;
            }

            public void SetName(string name)
            {
                _name = name;
            }

            public string GetName()
            {
                return _name;
            }
        }

        public class ThreadSafeSingleton
        {
            public ThreadSafeSingleton()
            {
                
            }

            private static ThreadSafeSingleton? _instance;
            public string Name { get; set; } = string.Empty;

            private static readonly object _lock = new object();

            /// <summary>
            /// 取得實體
            /// </summary>
            /// <param name="name">用來識別實體的參數</param>
            public static ThreadSafeSingleton GetInstance(string name)
            {
                if ( _instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ThreadSafeSingleton();
                            _instance.Name = name;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
