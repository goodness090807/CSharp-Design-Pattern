namespace DesignPattern.CreationalPatterns
{
    /// <summary>
    /// -----------------
    /// 抽象工廠模式
    /// -----------------
    /// </summary>
    public class AbstractFactory
    {
        private readonly ISedan _sedan;
        private readonly ISUV _suv;

        public AbstractFactory(ICarFactory carFactory)
        {
            _sedan = carFactory.CreateSedan();
            _suv = carFactory.CreateSUV();
        }

        public void ShowTime()
        {
            _sedan.TurnOnHeadLight();
            _suv.TurnOnHeadLight();
        }

        public void PrintCarName()
        {
            Console.WriteLine(_sedan.GetCarName());
        }

        public static void Run()
        {
            var honda = new HondaFactory();
            var hondaDemonstration = new AbstractFactory(honda);
            hondaDemonstration.ShowTime();
            hondaDemonstration.PrintCarName();

            var nissan = new NissanFactory();
            var nissanDemonstration = new AbstractFactory(nissan);
            nissanDemonstration.ShowTime();
            nissanDemonstration.PrintCarName();
        }

        #region 基礎架構
        /// <summary>
        /// 轎車介面
        /// </summary>
        public interface ISedan
        {
            void TurnOnHeadLight();
            string GetCarName();
        }

        /// <summary>
        /// 休旅車介面
        /// </summary>
        public interface ISUV
        {
            void TurnOnHeadLight();
        }

        public class HondaSedan : ISedan
        {
            public string GetCarName()
            {
                return "Honda Sedan車款";
            }

            public void TurnOnHeadLight()
            {
                Console.WriteLine("Honda的『轎車』開啟車燈了");
            }
        }

        public class HondaSUV : ISUV
        {
            public void TurnOnHeadLight()
            {
                Console.WriteLine("Honda的『休旅車』開啟車燈了");
            }
        }

        public class NissanSedan : ISedan
        {
            public string GetCarName()
            {
                return "Nissan Sedan車款";
            }

            public void TurnOnHeadLight()
            {
                Console.WriteLine("Nissan的『轎車』開啟車燈了");
            }
        }

        public class NissanSUV : ISUV
        {
            public void TurnOnHeadLight()
            {
                Console.WriteLine("Nissan的『休旅車』開啟車燈了");
            }
        }

        public interface ICarFactory
        {
            ISedan CreateSedan();
            ISUV CreateSUV();
        }

        public class HondaFactory : ICarFactory
        {
            public ISedan CreateSedan()
            {
                return new HondaSedan();
            }

            public ISUV CreateSUV()
            {
                return new HondaSUV();
            }
        }

        public class NissanFactory : ICarFactory
        {
            public ISedan CreateSedan()
            {
                return new NissanSedan();
            }

            public ISUV CreateSUV()
            {
                return new NissanSUV();
            }
        }
        #endregion
    }
}
