using System.Diagnostics;

namespace DesignPattern.SOLID
{
    /// <summary>
    /// -----------------
    /// 單一職責原則(SRP)
    /// -----------------
    /// 
    /// 一個方法或類別只做一件事，讓職責分離。
    /// 
    /// Journal和Persistence做不同的事
    /// 而不是把所有的事情都寫入一個Class
    /// 也就是單一職責原則
    /// </summary>
    public class No1_Single_Responsibility_Principle
    {
        public static void Run()
        {
            // 這是一個職責，專門加入資料
            var journal = new Journal();
            journal.AddEntry("2023-04-20 開始學習了Design Pattern");
            journal.AddEntry("這是一個單一職責原則的程式碼");

            // 額外操作：Print出資料
            Console.WriteLine(journal);

            // 這是另外一個職責，主要是將資料寫入檔案檔案
            var persistence = new Persistence();
            var fileName = "./Journal.txt";
            persistence.SaveToFile(journal, fileName, true);

            // 額外操作：開啟寫入後的檔案
            Process.Start("notepad.exe", Path.GetFullPath(fileName));
        }

        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");

                return count;
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            /// <summary>
            /// 不要在這裡做(違反單一職責)
            /// 雖然這樣說，但很多情況還是要由自己去界定範圍，
            /// 或許有些職責真的很相依這個類別，所以寫在這也是對的。
            /// </summary>
            //public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
            //{
            //    throw new NotImplementedException();
            //}
        }

        public class Persistence
        {
            public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
            {
                if (overwrite)
                {
                    File.WriteAllText(fileName, journal.ToString());
                }
                else
                {
                    if (File.Exists(fileName))
                    {
                        using (var sw = File.AppendText(fileName))
                        {
                            sw.WriteLine(journal.ToString());
                        }
                    }
                    else
                    {
                        using (var sw = File.CreateText(fileName))
                        {
                            sw.WriteLine(journal.ToString());
                        }
                    }
                }
            }
        }
    }
}
