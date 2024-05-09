using AtmApp.User;

namespace AtmApp
{
    public static class AccountManager
    {
        private static readonly IMessages Messages = Factory.CreateMessages();
        private static readonly IInputValidator Validator = Factory.CreateValidator();
        public static readonly List<UserAccount> Accounts = GetAccounts();

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
            UserAccount newAccount = new UserAccount { AccountNumber = accountNumber, Balance = balance, Pin = pin };
            Accounts.Add(newAccount);
            FileIO.write(new List<UserAccount> { newAccount });
        }

        public static void Update(UserAccount updatedAccount)
        {
            FileIO.Update(updatedAccount);
        }

        public static List<UserAccount> GetAccounts()
        {
            // question, should i handle exceptions that could occur if the data is messed up? 
            // or would it better for the app to crash if the core data is somehow corrupted?
            // cause what would it load, if it runs.
            return FileIO.Read();
        }

        public static void AddAccounts(List<UserAccount> accounts)
        {
            // Read existing accounts from CSV file
            List<UserAccount> existingAccounts = GetAccounts();

            // Filter accounts to be added that don't already exist
            List<UserAccount> newAccounts = accounts.Where(account => !existingAccounts.Any(existing => existing.AccountNumber == account.AccountNumber)).ToList();

            FileIO.write(newAccounts);
        }


        public static UserAccount? LoginAsUser()
        {
            int accountNumberLength = 10;
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
                else if (inputtedAccountNumber.ToString().Length < accountNumberLength)
                {
                    Messages.Invalid("Length, Enter a 10 digit number");
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
