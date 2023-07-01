namespace DesignPattern.SOLID
{
    /// <summary>
    /// -----------------
    /// 里氏替換原則 (LSP)
    /// -----------------
    /// 
    /// 一個類別應該都能被其繼承的類做替換，且不會影響程式運行的正確性
    /// 
    /// 以Rectangle為例，如果Width和Height沒有使用virtual，會導致程式運行的錯誤，
    /// 像是Square繼承只能使用new關鍵字，
    /// 但遇到Square指定為Rectangle類別時，
    /// 會只有變更Rectangle的設定
    /// 
    /// 如果使用virtual加上overwrite的話
    /// 程式則會先去看Rectangle是否有virtual，
    /// 有的話就會去看Square overwrite的實作
    /// </summary>
    public class No3_Liskov_Substitution_Principle
    {
        public static int Area(Rectangle rectangle) => rectangle.Width * rectangle.Height;

        public static void Run()
        {
            // 如果是使用Bad Pratice(也就是new的寫法，會導致錯誤)，詳細可看Bad Pratice
            Rectangle rt = new Square();
            rt.Width = 4;
            Console.WriteLine($"{rt} has area {Area(rt)}");
        }

        public class Rectangle
        {
            public Rectangle()
            {
            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            /// <summary>
            /// 透過virtual，程式會先去找繼承的class
            /// 並執行繼承的動作，這樣就不會導致繼承的類別如果使用Rectangle，卻只有設定到Rectangle的變數了
            /// </summary>
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        /// <summary>
        /// Bad Pratice
        /// </summary>
        //public class Square : Rectangle
        //{
        //    // 使用new關鍵字時，如果當Squre指定變數類別為Rectangle，會導致異常
        //    // 主因是因為我們變更的是Rectangle的類別，而不是Square的類別
        //    public new int Width { set { base.Width = base.Height = value; } }
        //    public new int Height { set { base.Width = base.Height = value; } }
        //}

        /// <summary>
        /// Best Pratice
        /// </summary>
        public class Square : Rectangle
        {
            public override int Width { set { base.Width = base.Height = value; } }
            public override int Height { set { base.Width = base.Height = value; } }
        }
    }
}
