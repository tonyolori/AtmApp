
using AtmApp;
using AtmApp.Messages;
using AtmApp.User;

List<string> possibleEndOptions = new() { "yes", "y", "n", "no" };
List<int> switchCaseList = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
IMessages outputmessages = Factory.CreateMessages();
IInputValidator validator = Factory.CreateValidator();


outputmessages.WelcomeGreeting();

int choice = validator.LoopValidateFormat(int.Parse, outputmessages);

if (choice == 0)
{
    createAccount();
}
else if (choice != 1)
{
    return;
}
UserAccount? user = AccountManager.LoginAsUser();

Console.WriteLine("Please select which account you would be working with");

//project ideas
//tic tac toe
//link shortener

if (user == null)
{
    return;
}

while (true)
{
    List<uint> accountNumberList = AccountManager.GetAccountNumberlist();

    Console.WriteLine("\nWould you like to:");
    Console.WriteLine("1. Deposit \n2. Withdraw \n3. Check Balance" +
        " \n4. Change Pin \n5. Transfer \n6. logout \n7. Creation \n8. Cancel");


    // make sure it is both an int and within the accepted list of inputs
    while (!validator.ValidateFormat(int.Parse, Console.ReadLine(), out choice)
        || !validator.ValidateAgainstList(choice, switchCaseList))
    {
        outputmessages.Invalid("number");
    }

    //extra whitespace
    Console.WriteLine();


    switch (choice)
    {
        case 1: // Deposit branch 
            AccountManager.PrintBalance(user);
            outputmessages.Input("deposit amount");

            //makes sure the user enters a proper uint and returns the uint
            uint depositAmount = validator.LoopValidateFormat(uint.Parse, outputmessages);

            try
            {
                user.Deposit(depositAmount);

                outputmessages.Success("Deposit");
            }
            catch
            {
                outputmessages.Error("Deposit");

            }

            break;

        case 2: // Withdrawal branch
            AccountManager.PrintBalance(user);
            outputmessages.Input("withdrawal amount");

            //get a valid uint number from the user
            uint withdrawAmount = validator.LoopValidateFormat(uint.Parse, outputmessages);


            try
            {
                outputmessages.Success("Withdrawal");
            }
            catch
            {
                outputmessages.Error("Withdrawal");
            }

            break;

        case 3: // Check balance
            AccountManager.PrintBalance(user);
            break;

        case 4:// changepin
            outputmessages.Input("new Pin");

            uint newPin = validator.LoopValidateFormat(uint.Parse, outputmessages);


            //make sure the new pin is not the former pin
            if (user.MatchPin(newPin))
            {
                outputmessages.Error("Pin change");
                Console.WriteLine("new pin cannot be the same as old pin");
            }
            else
            {
                user.ChangePin(newPin);
                outputmessages.Success("Pin change");
            }
            break;

        // Transfer
        case 5:
            //receive the number 
            uint accountNumber = 0;

            UserAccount receivingUser;

            while (true)
            {
                //Console.WriteLine("Enter the account number you would like to transfer to");
                outputmessages.Input("receiving account number");

                accountNumber = validator.LoopValidateFormat(uint.Parse, outputmessages);


                //if the number exists and is not yours then you can exit the loop
                if (validator.ValidateAgainstList(accountNumber, AccountManager.GetAccountNumberlist())
                    && accountNumber != user.AccountNumber)
                {
                    receivingUser = AccountManager.Accounts.First(account => account.AccountNumber == accountNumber);
                    break;
                }

                if (!validator.ValidateAgainstList(accountNumber, AccountManager.GetAccountNumberlist()))
                {
                    outputmessages.Invalid("Length, Enter a 10 digit number");
                }
                outputmessages.Invalid("account number");
            }


            AccountManager.PrintBalance(user);
            outputmessages.Input("transfer amount");

            uint transferAmount = validator.LoopValidateFormat(uint.Parse, outputmessages);

            //confirm transfer succeessful then break
            if (user.Transfer(transferAmount, receivingUser))
            {
                outputmessages.Success("Transfer");
                AccountManager.PrintBalance(user);
                break;
            }
            else
            {
                outputmessages.Error("Transfer");
            }

            break;
        case 6:
            user = AccountManager.LoginAsUser();
            if (user == null)
                return;
            break;
        case 7:
            createAccount();
            break;
        case 8:
            break;
        default:
            break;
    }

    outputmessages.Custom("\nwould you like to complete another transaction Y/N");
    string? response = Console.ReadLine();

    //come fix warning
    while (!validator.ValidateAgainstList<string>(response?.ToLower(), possibleEndOptions))
    {
        Console.WriteLine("invalid Input!, Try again");
        response = Console.ReadLine();
    }

    if (response.ToLower() == "n" || response.ToLower() == "no")
    {
        break;
    }

}


void createAccount()
{
    outputmessages.Input("account number");
    uint inputtedAccountNumber = validator.LoopValidateFormat(uint.Parse, outputmessages);
    if (validator.ValidateAgainstList(inputtedAccountNumber, AccountManager.GetAccountNumberlist()))
    {
        outputmessages.DisplayValidationError("Account number, Account number exists already ");
        outputmessages.Error("Account Creation");
        return;
    }
    else if (!validator.ValidateLength(inputtedAccountNumber, 10))
    {
        outputmessages.DisplayValidationError("Account number, account number is not 10 digits ");
        outputmessages.Error("Account Creation");
        return;
    }

    outputmessages.Input("Balance, 0 for an Empty starting balance");
    uint inputtedBalance = validator.LoopValidateFormat(uint.Parse, outputmessages);


    outputmessages.Input("4 digit Pin");
    uint inputtedPin = validator.LoopValidateFormat(uint.Parse, outputmessages);
    if (!validator.ValidateLength(inputtedPin, 4))
    {
        outputmessages.DisplayValidationError("Pin, Enter a 4 digit pin");
        outputmessages.Error("Account Creation");
        return;
    }

    AccountManager.Createuser(inputtedAccountNumber, inputtedBalance, inputtedPin);

} 
