
using AtmApp;

List<UserAccount> accounts = new List<UserAccount>{
            //accountNumber, balance, pin
            new UserAccount(1001, 50000, 1234),
            new UserAccount(1002, 100000, 5678),
            new UserAccount(1003, 200000, 9012),
            new UserAccount(1004, 200000, 9032),

        };

List<uint> accountNumberList = UserAccount.GetAccountNumberlist(accounts);

Console.WriteLine("Welcome to the Atm");
Console.WriteLine("Please select which account you would be working with");

UserAccount? user = LoginAsUser();

if (user == null)
{
    return;
}

while (true)
{
    int choice;
    uint newPin, inputtedAmount;



    Console.WriteLine("\nWould you like to:");
    Console.WriteLine("1. Deposit \n2. Withdraw \n3. Check Balance \n4. Change Pin \n5. Transfer \n6. login to another account\n7. Cancel ");


    // make sure it is both an int and within the accepted list of inputs
    while (!int.TryParse(Console.ReadLine(), out choice) && ValidateUserInput<int>(choice, new List<int> { 1, 2, 3, 4, 5 }))
    {
        Console.WriteLine("invalid input");
    }
    Console.WriteLine();


    switch (choice)
    {
        case 1: // Deposit branch 
            Console.WriteLine($"Your account balance is {user.GetBalance()}");
            Console.WriteLine("Please input your deposit amount");

            while (!uint.TryParse(Console.ReadLine(), out inputtedAmount))

            {
                Console.WriteLine("Input a valid amount");
            }

            if (user.Deposit(inputtedAmount))
            {
                Console.WriteLine("Amount deposited successfully");
            }
            else
            {
                Console.WriteLine("error with deposit");
            }

            break;

        case 2: // Withdrawal branch
            Console.WriteLine($"Your account balance is {user.GetBalance()}");
            Console.WriteLine("Please input your withdrawal amount");

            //while not a valid int 
            while (!uint.TryParse(Console.ReadLine(), out inputtedAmount))

            {
                Console.WriteLine("Input a valid amount");
            }

            if (user.Withdraw(inputtedAmount))
            {
                Console.WriteLine("Amount Withdrawn successfully");
            }
            else
            {
                Console.WriteLine("Withdrawal failed");
            }

            break;

        case 3: // Check balance
            Console.WriteLine($"Your account balance is {user.GetBalance()}");
            break;

        case 4:// changepin
            Console.WriteLine("Enter Your new pin");

            while (!uint.TryParse(Console.ReadLine(), out newPin))

            {
                Console.WriteLine("Input a valid Pin, Must be a positive number");
            }

            //make sure the new pin is not the former pin
            if (user.MatchPin(newPin))
            {
                Console.WriteLine("new pin cannot be the same as old pin");
            }
            else
            {
                user.ChangePin(newPin);
                Console.WriteLine("Pin changed Sucessfully");
            }
            break;

        case 5:// Transfer
            //receive the number 
            uint accountNumber=0;
            
            uint transferAmount = 0;
            UserAccount receivingUser;



            while (true)
            {
                Console.WriteLine("Enter the account number you would like to transfer to");

                bool result = false;
                while (!result)
                {
                    result = uint.TryParse(Console.ReadLine(), out accountNumber);
                    if (!result)
                    {
                        Console.WriteLine("Invalid Number");
                    }
                }

                //if the number exists then you can exit the loop
                if (ValidateUserInput(accountNumber, UserAccount.GetAccountNumberlist(accounts)))
                {
                    receivingUser = accounts.First(account => account.GetAccountNumber() == accountNumber);
                    break;
                }
                    Console.WriteLine("invalid account number");
            }


            while (true)
            {
                Console.WriteLine("\nEnter the amount the amount you would like to transfer");
                Console.WriteLine($"Your current balance is {user.GetBalance()}");
                bool result = false;
                while (!result)
                {
                    result = uint.TryParse(Console.ReadLine(), out transferAmount);
                    if (!result)
                    {
                        Console.WriteLine("Invalid amount");
                    }
                }

                //if the transfer works then you can break
                if (user.Transfer(transferAmount, receivingUser))
                {
                    Console.WriteLine($"Transfer Succesfull, new balance is {user.GetBalance()}");
                    break;
                }

            }

            break;
        case 6:
            user = LoginAsUser();
            if (user == null)
                return;
            break;
        case 7:
            break;
        default:
            break;
    }

    Console.WriteLine("\nwould you like to complete another transaction Y/N");
    string response = Console.ReadLine();

    while (!ValidateUserInput<string>(response, new List<string> { "yes", "y", "Y", "N", "n", "no" }))
    {
        Console.WriteLine("invalid Input!, Try again");
        response = Console.ReadLine();
    }

    if (response.ToLower() == "n" || response.ToLower() == "no")
    {
        break;
    }

}
static bool ValidateUserInput<T>(T givenInput, List<T> acceptedInputs)
{

    foreach (T e in acceptedInputs)
    {

        if (givenInput == null)
        {
            return false;
        }

        else if (givenInput.Equals(e))
        {
            return true;
        }
    }
    return false;
}

    UserAccount? LoginAsUser()
    {
        const int MaxLoginAttempts = 5;

        uint inputtedAccountNumber;
        Console.WriteLine("Please enter your account number:");

        while (!uint.TryParse(Console.ReadLine(), out inputtedAccountNumber) || !accountNumberList.Contains(inputtedAccountNumber))
        {
            Console.WriteLine("Invalid account number or account number does not exist. Please try again:");
        }

        UserAccount user = accounts.First(account => account.GetAccountNumber() == inputtedAccountNumber);

        Console.WriteLine("Please enter your PIN:");
        int tries = MaxLoginAttempts;

        while (tries > 0)
        {
            if (!uint.TryParse(Console.ReadLine(), out uint entryPin))
            {
                Console.WriteLine("Invalid PIN format. Please enter a numeric PIN:");
                continue;
            }

            if (user.MatchPin(entryPin))
            {
                Console.WriteLine("Login successful. Welcome!");
                return user;
            }

            tries--;
            Console.WriteLine($"Incorrect PIN. {tries} attempts remaining.");
        }

        Console.WriteLine("Maximum login attempts exceeded. Card swallowed!");
        return null; // or throw an exception indicating maximum attempts exceeded
    }

