namespace DesignPattern.StructuralPatterns
{
    /// <summary>
    /// -----------------
    /// 外觀模式
    /// -----------------
    /// 
    /// 它提供了一個簡單而統一的介面，用於操作一個複雜子系統。
    /// 透過Facade模式，可以隱藏子系統的複雜性，並提供一個更容易使用的介面，以方便客戶端程式碼使用。
    /// </summary>
    public class Facade
    {
        public static void Run()
        {
            var bankAccountService = new BankAccountFacade();
            bankAccountService.UserTakeMoney("test", "test", 100);
        }

        public class BankAccountFacade
        {
            private readonly AccountVerifier _accountVerifier;
            private readonly AccountProcessor _accountProcessor;

            /// <summary>
            /// 建構函式
            /// 可以從外部傳入服務，或是內部自己建立服務
            /// 這邊是自己建立
            /// </summary>
            public BankAccountFacade()
            {
                _accountVerifier = new AccountVerifier();
                _accountProcessor = new AccountProcessor();
            }

            public void UserTakeMoney(string account, string password, decimal amount)
            {
                if (!_accountVerifier.Login(account, password))
                {
                    Console.WriteLine("帳號或密碼錯誤");
                    return;
                }

                if (!_accountVerifier.VerifyAccount(account))
                {
                    Console.WriteLine("帳號未開通");
                    return;
                }

                _accountProcessor.Withdraw(account, amount);
            }
        }

        /// <summary>
        /// 子系統的類別之一 - 驗證帳戶
        /// 他底下有很多功能是跟驗證帳號有關
        /// </summary>
        public class AccountVerifier
        {
            public bool Login(string account, string password)
            {
                return account == "test" && password == "test";
            }
            public bool VerifyAccount(string account)
            {
                return account == "test";
            }
        }

        /// <summary>
        /// 子系統的類別之二 - 帳戶存取
        /// 他底下有很多功能是跟存款和提款有關的
        /// </summary>
        public class AccountProcessor
        {
            public void Withdraw(string account, decimal amount)
            {
                Console.WriteLine($"{account} take {amount} dollars");
            }

            public void Deposit(string account, decimal amount)
            {
                Console.WriteLine($"{account} deposit {amount} dollars");
            }
        }
    }
}
