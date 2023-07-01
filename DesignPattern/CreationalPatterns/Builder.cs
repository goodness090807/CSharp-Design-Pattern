namespace DesignPattern.CreationalPatterns
{
    public class Builder
    {

        public static void Run()
        {
            var asusComputer = new ASUSComputerBuilder("ASUS 包裝機");

            var director = new ComputerDirector(asusComputer);
            var computer = director.MakeComputer(new() { "羅技MX Anywhere 2S恣意優游行動無線滑鼠", "羅技 K120 USB有線鍵盤" });
            computer.PrintDescription();

            Console.WriteLine("");

            // 自訂的電腦
            var customComputerBuilder = new ComputerBuilder("客製化組裝機");
            var customComputer = customComputerBuilder.SetMotherboard("自己買的主機板")
                                    .SetGpu("自己買的GPU")
                                    .AddAccessory("耳機")
                                    .AddAccessory("麥克風")
                                    .Build();

            customComputer.PrintDescription();
        }

        public class Computer
        {
            private string Name { get; }
            public string? Motherboard { get; set; }
            public string? CPU { get; set; }
            public string? GPU { get; set; }
            public string? Memory { get; set; }
            public string? Power { get; set; }
            public string? Disk { get; set; }
            public List<string> Accessories { get; set; } = new List<string>();

            public Computer(string name)
            {
                Name = name;
            }

            public void PrintDescription()
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("電腦規格");
                Console.WriteLine("------------------------------");
                Console.WriteLine($"名稱：{Name}");
                Console.WriteLine($"主機板：{Motherboard}");
                Console.WriteLine($"CPU：{CPU}");
                Console.WriteLine($"GPU：{GPU}");
                Console.WriteLine($"記憶體：{Memory}");
                Console.WriteLine($"電源供應器：{Power}");
                Console.WriteLine($"硬碟：{Disk}");

                if (Accessories.Any())
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("附贈的周邊設備");
                    Console.WriteLine("------------------------------");
                    foreach (var accessory in Accessories)
                    {
                        Console.WriteLine($"{accessory}");
                    }
                }
            }
        }

        public interface IComputerBuilder
        {
            IComputerBuilder SetMotherboard();
            IComputerBuilder SetCpu();
            IComputerBuilder SetGpu();
            IComputerBuilder SetMemory();
            IComputerBuilder SetPower();
            IComputerBuilder SetDisk();
            IComputerBuilder SetAccessories(List<string> accessories);
            Computer Build();
        }

        #region 建立各種Builder
        /// <summary>
        /// 這邊是建立自訂的Builder
        /// </summary>
        public class ComputerBuilder
        {
            private readonly Computer _computer;

            public ComputerBuilder(string computerName)
            {
                _computer = new Computer(computerName);
            }

            public ComputerBuilder SetMotherboard(string motherboard)
            {
                _computer.Motherboard = motherboard;
                return this;
            }

            public ComputerBuilder SetCpu(string cpu)
            {
                _computer.CPU = cpu;
                return this;
            }

            public ComputerBuilder SetGpu(string gpu)
            {
                _computer.GPU = gpu;
                return this;
            }

            public ComputerBuilder SetMemory(string memory)
            {
                _computer.Memory = memory;
                return this;
            }

            public ComputerBuilder SetPower(string power)
            {
                _computer.Power = power;
                return this;
            }

            public ComputerBuilder SetDisk(string disk)
            {
                _computer.Disk = disk;
                return this;
            }

            public ComputerBuilder AddAccessory(string accessoryName)
            {
                _computer.Accessories.Add(accessoryName);
                return this;
            }

            public Computer Build()
            {
                return _computer;
            }
        }

        /// <summary>
        /// 這邊是只有針對ASUS去建立電腦的Builder
        /// 這種的好處是可以使用Director Pattern來做呼叫
        /// </summary>
        public class ASUSComputerBuilder : IComputerBuilder
        {
            private readonly Computer _computer;
            private readonly string _motherboard = "技嘉 B650 AORUS ELITE";
            private readonly string _cpu = "AMD R5-7600X";
            private readonly string _gpu = "NVIDIA A30 Tensor 核心 GPU";
            private readonly string _memory = "Micron Crucial 美光 DDR4 3200/16G 桌上型記憶體";
            private readonly string _disk = "美光Micron Crucial MX500 1TB SATAⅢ 固態硬碟";
            private readonly string _power = "PURE POWER 11 500W 80+金牌 電源供應器";

            public ASUSComputerBuilder(string computerName)
            {
                _computer = new Computer(computerName);
            }

            public Computer Build()
            {
                return _computer;
            }

            /// <summary>
            /// 也可以額外製作這種由外部帶進來的變數，增添一些變化
            /// </summary>
            public IComputerBuilder SetAccessories(List<string> accessories)
            {
                _computer.Accessories = accessories;
                return this;
            }

            public IComputerBuilder SetCpu()
            {
                _computer.CPU = _cpu;
                return this;
            }

            public IComputerBuilder SetDisk()
            {
                _computer.Disk = _disk;
                return this;
            }

            public IComputerBuilder SetGpu()
            {
                _computer.GPU = _gpu;
                return this;
            }

            public IComputerBuilder SetMemory()
            {
                _computer.Memory = _memory;
                return this;
            }

            public IComputerBuilder SetMotherboard()
            {
                _computer.Motherboard = _motherboard;
                return this;
            }

            public IComputerBuilder SetPower()
            {
                _computer.Power = _power;
                return this;
            }
        }
        #endregion

        /// <summary>
        /// 透過Director來做到統一的使用Builder
        /// </summary>
        public class ComputerDirector
        {
            private IComputerBuilder _computerBuilder;

            public ComputerDirector(IComputerBuilder computerBuilder)
            {
                _computerBuilder = computerBuilder;
            }

            public Computer MakeComputer(List<string> accessories)
            {
                return _computerBuilder.SetMotherboard()
                    .SetCpu()
                    .SetGpu()
                    .SetMemory()
                    .SetPower()
                    .SetDisk()
                    .SetAccessories(accessories)
                    .Build();
            }
        }
    }
}
