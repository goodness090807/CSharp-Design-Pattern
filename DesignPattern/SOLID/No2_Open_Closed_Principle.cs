namespace DesignPattern.SOLID
{
    /// <summary>
    /// -----------------
    /// 開放封閉原則(OCP)
    /// -----------------
    /// 
    /// 必須可以開放擴充，
    /// 但要封閉修改。
    /// 
    /// ------------
    /// 相關技巧
    /// ------------
    /// 1. 使用繼承
    /// 2. 使用enterprise pattern、或是叫做specification pattern，可以讓我們避免違反開放封閉原則
    /// 3. 使用泛型型別，可以帶入多種類別，不會只侷限於一種類別
    /// </summary>
    public class No2_Open_Closed_Principle
    {
        public static void Run()
        {
            var apple = new Product("Apple", Color.White, Size.Small);
            var googlePixel = new Product("Google Pixel", Color.Black, Size.Small);
            var samsung = new Product("Samsung", Color.Black, Size.Large);

            var products = new Product[] {apple, googlePixel, samsung};

            // (較不好)這邊用一般寫法，擴充程度較難
            //foreach (var product in ProductFilter.FilterByColor(products, Color.Black))
            //{
            //    Console.WriteLine($"{product.Name} is Black");
            //}

            var productFilter = new ProductFilter();

            Console.WriteLine("---篩選Color---");
            foreach (var product in productFilter.Filter(products, new ColorSpecification(Color.Black)))
            {
                Console.WriteLine($"{product.Name} is Black");
            }

            Console.WriteLine("---篩選Size---");
            foreach (var product in productFilter.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine($"{product.Name} is Large");
            }

            // 如果要兩者結合的篩選，擴充也很方便，不會動到原本的程式
            // 也可以很好的擴充，不局限於Product
            Console.WriteLine("---篩選Color和Size---");
            foreach (var product in productFilter.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Black), 
                    new SizeSpecification(Size.Small))))
            {
                Console.WriteLine($"{product.Name} is Black and Small");
            }
        }

        #region 基本規格
        public enum Color
        {
            Black, White, Gray, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string? Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string? name, Color color, Size size)
            {
                // 建構子可以寫一些判斷
                if (name == null) throw new ArgumentNullException(nameof(name));

                Name = name;
                Color = color;
                Size = size;
            }
        }
        #endregion

        #region Bad Pratice

        /// <summary>
        /// 不好的寫法，能發現違反了開放封閉原則
        /// 當要篩選產品時，寫了一個篩選，但是如果需求要多一個篩選，就需要再擴充
        /// </summary>
        //public class ProductFilter
        //{
        //    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        //    {
        //        foreach (var product in products)
        //        {
        //            if (product.Size == size) yield return product;
        //        }
        //    }
        //    public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        //    {
        //        foreach (var product in products)
        //        {
        //            if (product.Color == color) yield return product;
        //        }
        //    }

        //    /// <summary>
        //    /// 不好的寫法，因為要擴充又動了這個Class
        //    /// </summary>
        //    public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        #endregion

        #region Best Pratice 從這邊之後是符合開放封閉原則的較佳實踐

        ///<summary>
        /// 定義規格介面
        ///</summary>
        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        /// <summary>
        /// 定義篩選介面
        /// </summary>
        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
        }

        /// <summary>
        /// 撰寫規格(Color的部分)
        /// 主要是繼承ISpecification，並放入Product
        /// 而我們只要實作Interface的方法
        /// </summary>
        public class ColorSpecification : ISpecification<Product>
        {
            private readonly Color _color;

            public ColorSpecification(Color color)
            {
                _color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == _color;
            }
        }

        public class SizeSpecification: ISpecification<Product>
        {
            private readonly Size _size;

            public SizeSpecification(Size size)
            {
                _size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Size == _size;
            }
        }

        /// <summary>
        /// 這邊的擴充是可以不只限於Product，可以擴充更多項目
        /// </summary>
        public class AndSpecification<T> : ISpecification<T>
        {
            private readonly ISpecification<T> _first;
            private readonly ISpecification<T> _second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                _first = first ?? throw new ArgumentNullException(nameof(first));
                _second = second ?? throw new ArgumentNullException(nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return _first.IsSatisfied(t) && _second.IsSatisfied(t);
            }
        }

        public class ProductFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
            {
                foreach(var product in  items)
                {
                    if (specification.IsSatisfied(product))
                        yield return product;
                }
            }
        }
        #endregion
    }
}
