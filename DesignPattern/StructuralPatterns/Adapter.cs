namespace DesignPattern.CreationalPatterns
{
    public class Adapter
    {
        /// <summary>
        /// -----------------
        /// 適配器模式
        /// 
        /// 更多參考：https://ianjustin39.github.io/ianlife/design-pattern/adapter-pattern/
        /// -----------------
        /// </summary>
        public static void Run()
        {
            var otherService = new Service();
            var adaperService = new ServiceAdapter(otherService);
            var result = adaperService.GetRequest();

            Console.WriteLine(result);
        }

        public class ServiceAdapter
        {
            private readonly Service _service;

            public ServiceAdapter(Service service)
            {
                _service = service;
            }

            public string GetRequest()
            {
                return _service.GetSpecificRequest();
            }
        }

        public class Service
        {
            public string GetSpecificRequest()
            {
                return "Specific request.";
            }
        }
    }
}
