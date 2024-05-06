using AtmApp.User;

namespace AtmApp
{
    public static class AccountManager
    {
        private static readonly IMessages Messages = Factory.CreateMessages();
        private static readonly IInputValidator Validator = Factory.CreateValidator();
        public static readonly List<UserAccount> Accounts = new (){
            //accountNumber, balance, pin
            new UserAccount{AccountNumber = 1234567892, Balance =  100000, Pin = 5678 },
            new UserAccount{AccountNumber = 1234567893, Balance =  200000, Pin = 9012 },
            new UserAccount{AccountNumber = 1234567894, Balance =  200000, Pin = 9032 },
            new UserAccount{AccountNumber = 1234567891, Balance =  50000, Pin = 1234 },
        };

        public static List<uint> GetAccountNumberlist()
        {
            List<uint> accountNumbers = new();

            foreach (var account in Accounts)
            {
                accountNumbers.Add(account.AccountNumber);
            }

            return accountNumbers;
        }

        public static void Createuser(uint accountNumber, uint balance, uint pin)
        {
            Accounts.Add(new UserAccount { AccountNumber = accountNumber, Balance = balance, Pin = pin });
        }


        public static UserAccount? LoginAsUser()
        {
            int accountNumberlength = 10;
            List<uint> accountNumberList = GetAccountNumberlist();

            uint inputtedAccountNumber;
            Messages.Custom(" ");
            Messages.Custom("LOGIN");
            Messages.Input("account number");

            
            while (true) { 
            
                //todo: remove the if statement, make the account number validation happen at the user line
                if (!Validator.ValidateFormat(uint.Parse, Console.ReadLine(), out inputtedAccountNumber))
                {
                    Messages.Invalid("Account Number ");
                }
                else if (inputtedAccountNumber.ToString().Length < accountNumberlength)
                {
                    Messages.Invalid("Length, Enter a 10 digit number.");
                }
                else if(!accountNumberList.Contains(inputtedAccountNumber))
                {
                    Messages.Custom("Account number does not exist");
                }
                else
                {
                    break;
                }

            }

            // match account number with the user from the accounts list
            var user = Accounts.FirstOrDefault(account => account.AccountNumber == inputtedAccountNumber);
            if (user == null) {
                return null;
            }

            UserAccount? userAccount = user.Login();
            return userAccount;
            
        }

        public static void PrintBalance(UserAccount account)
        {
            Messages.Custom($"Your account balance is {account.Balance}");
        }

    }
}
