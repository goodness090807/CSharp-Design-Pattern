using System.Reflection.Metadata;

namespace DesignPattern.SOLID
{
    /// <summary>
    /// -----------------
    /// 介面隔離原則 (ISP)
    /// -----------------
    /// 
    /// 模組與模組之間的依賴，不應有用不到的功能可以被對方呼叫
    /// </summary>
    public class No4_Interface_Segregation_Principle
    {
        public static void Run()
        {
            throw new NotImplementedException();
        }

        public class Document
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public interface IMachine
        {
            void Print(Document d);
            void Fax(Document d);
            void Scan(Document d);
        }

        /// <summary>
        /// 如果有一個方法，他要實作的項目就是IMachine所有要做的事
        /// 使用這個介面是可以的
        /// </summary>
        public class MultiFunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                // 有實作
            }

            public void Fax(Document d)
            {
                // 有實作
            }

            public void Scan(Document d)
            {
                // 有實作
            }
        }

        /// <summary>
        /// Bad Pratice
        /// 這裡面有兩個方法沒有實作，如果使用IMachine這個介面就過度使用了
        /// </summary>
        //public class OldFashionedPrinter : IMachine
        //{
        //    public void Print(Document d)
        //    {
        //        // 有實作的方法
        //    }

        //    public void Fax(Document d)
        //    {
        //        // 沒實作
        //        throw new System.NotImplementedException();
        //    }

        //    public void Scan(Document d)
        //    {
        //        // 沒實作
        //        throw new System.NotImplementedException();
        //    }
        //}

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        /// <summary>
        /// 這樣我們要做Printer就可以只做一件事了
        /// </summary>
        public class Printer : IPrinter
        {
            public void Print(Document d)
            {

            }
        }

        /// <summary>
        /// 也可以組合起來
        /// </summary>
        public class PhotoCopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 也可以像這樣interface做結合
        /// </summary>
        public interface IMultiFunctionDevice : IPrinter, IScanner //
        {

        }

        /// <summary>
        /// 這邊就使用結合過的interface
        /// </summary>
        public struct MultiFunctionMachine : IMultiFunctionDevice
        {
            // compose this out of several modules
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                if (printer == null)
                {
                    throw new ArgumentNullException(paramName: nameof(printer));
                }
                if (scanner == null)
                {
                    throw new ArgumentNullException(paramName: nameof(scanner));
                }
                this.printer = printer;
                this.scanner = scanner;
            }

            public void Print(Document d)
            {
                printer.Print(d);
            }

            /// <summary>
            /// 這種方式稱為 decorator pattern
            /// </summary>
            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }
    }
}
