using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.SOLID.No5_Dependency_Inversion_Principle;

namespace DesignPattern.SOLID
{
    /// <summary>
    /// -----------------
    /// 依賴反向原則 (DIP)
    /// -----------------
    /// 
    /// High-level的模組，不應該直接依賴於low-level的模組。兩者應該依賴於抽象
    /// 抽象不應該依賴細節。細節應該依賴抽象
    /// 
    /// 好文：
    /// https://medium.com/@f40507777/%E4%BE%9D%E8%B3%B4%E5%8F%8D%E8%BD%89%E5%8E%9F%E5%89%87-dependency-inversion-principle-dip-bc0ba2e3a388
    /// https://www.jyt0532.com/2020/03/24/dip/
    /// </summary>
    public class No5_Dependency_Inversion_Principle
    {
        /// <summary>
        /// Bad Practice
        /// 我在進入一個類別的時候，希望找出Service為Camera的機器
        /// 但這個寫法有一個問題
        /// 我們在參數帶入的是一個類別，而不是抽象Interface，這樣如果在替換會有依賴性太強的問題
        /// </summary>
        //public No5_Dependency_Inversion_Principle(MachineService machineService)
        //{
        //    var canUseCameraMachine = machineService.GetServices.Where(x => x.Service == "Camera");

        //    foreach (var machine in canUseCameraMachine)
        //    {
        //        Console.WriteLine(machine.Name);
        //    }
        //}

        /// <summary>
        /// Best Practice
        /// 透過抽象，我們可以只要了解抽象有做什麼事就好了
        /// 而不會知道MachineService到底做了什麼
        /// 且如果要替換搜尋方式，也可以不用改MachineService
        /// 而是可以再製作一個類別，並繼承IMachineSearch，並實作內容就可以套用了
        /// 透過這樣的方式，可以很好的將物件與物件之間的關聯做解偶，降低偶合度
        /// </summary>
        public No5_Dependency_Inversion_Principle(IMachineSearch machineSearch)
        {
            foreach (var machine in machineSearch.GetMachinesByService("Camera"))
            {
                Console.WriteLine(machine.Name);
            }
        }

        public static void Run()
        {
            var desktop = new Machine() { Name = "ASUS", Service = "Programming"};
            var cellphone = new Machine() { Name = "IPhone", Service = "Camera" };

            var machineService = new MachineService();
            machineService.AddService(desktop);
            machineService.AddService(cellphone);

            new No5_Dependency_Inversion_Principle(machineService);
        }

        /// <summary>
        /// 這邊有一個機器的Model
        /// </summary>
        public class Machine
        {
            public string? Name { get; set; }
            public string? Service { get; set; }
        }

        #region Bad Pratice

        /// <summary>
        /// 這邊有一個機器服務物件，它會記錄機器和提供的服務和回傳所有服務的結果
        /// </summary>
        //public class MachineService
        //{
        //    private readonly List<Machine> _machines = new List<Machine>();

        //    public void AddService(Machine machine)
        //    {
        //        _machines.Add(machine);
        //    }

        //    public IEnumerable<Machine> GetServices => _machines;
        //}
        #endregion

        #region Best Pratice
        public interface IMachineSearch
        {
            IEnumerable<Machine> GetMachinesByService(string service);
        }

        public class MachineService : IMachineSearch
        {
            private readonly List<Machine> _machines = new List<Machine>();

            public void AddService(Machine machine)
            {
                _machines.Add(machine);
            }

            /// <summary>
            /// 改成實作這個方法，而不是直接回傳全部
            /// </summary>
            public IEnumerable<Machine> GetMachinesByService(string service)
            {
                return _machines.Where(x => x.Service == service);
            }
        }
        #endregion
    }
}
