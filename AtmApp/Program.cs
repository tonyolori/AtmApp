
using AtmApp;
using AtmApp.Messages;
using AtmApp.User;

List<string> possibleEndOptions = new() { "yes", "y", "n", "no" };
List<int> switchCaseList = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
IMessages outputMessages = Factory.CreateMessages();
IInputValidator validator = Factory.CreateValidator();
while (true)
{
    outputMessages.WelcomeGreeting();

    int choice = validator.LoopValidateFormat(int.Parse, outputMessages);

    switch (choice) 
    {
        case 1:
            if (!createAccount())
            {
                continue;
            }
            break;
        case 2:
            break;
        case 0:
           return;
        default:
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
            " \n4. Change Pin \n5. Transfer \n6. logout \n7. Account Creation \n8. Cancel");


        // make sure it is both an int and within the accepted list of inputs
        while (!validator.ValidateFormat(int.Parse, Console.ReadLine(), out choice)
            || !validator.ValidateAgainstList(choice, switchCaseList))
        {
            outputMessages.Invalid("number");
        }

        //extra whitespace
        Console.WriteLine();


        switch (choice)
        {
            case 1: // Deposit branch 
                AccountManager.PrintBalance(user);
                outputMessages.Input("deposit amount");

                //makes sure the user enters a proper uint and returns the uint
                uint depositAmount = validator.LoopValidateFormat(uint.Parse, outputMessages);

                try
                {
                    user.Deposit(depositAmount);

                    outputMessages.Success("Deposit");
                }
                catch
                {
                    outputMessages.Error("Deposit");

                }

                break;

            case 2: // Withdrawal branch
                AccountManager.PrintBalance(user);
                outputMessages.Input("withdrawal amount");

                //get a valid uint number from the user
                uint withdrawAmount = validator.LoopValidateFormat(uint.Parse, outputMessages);


                try
                {
                    outputMessages.Success("Withdrawal");
                }
                catch
                {
                    outputMessages.Error("Withdrawal");
                }

                break;

            case 3: // Check balance
                AccountManager.PrintBalance(user);
                break;

            case 4:// changepin
                outputMessages.Input("new Pin");

                uint newPin = validator.LoopValidateFormat(uint.Parse, outputMessages);


                //make sure the new pin is not the former pin
                if (user.MatchPin(newPin))
                {
                    outputMessages.Error("Pin change");
                    Console.WriteLine("new pin cannot be the same as old pin");
                }
                else
                {
                    user.ChangePin(newPin);
                    outputMessages.Success("Pin change");
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
                    outputMessages.Input("receiving account number");

                    accountNumber = validator.LoopValidateFormat(uint.Parse, outputMessages);


                    //if the number exists and is not yours then you can exit the loop
                    if (validator.ValidateAgainstList(accountNumber, AccountManager.GetAccountNumberlist())
                        && accountNumber != user.AccountNumber)
                    {
                        receivingUser = AccountManager.Accounts.First(account => account.AccountNumber == accountNumber);
                        break;
                    }

                    if (!validator.ValidateAgainstList(accountNumber, AccountManager.GetAccountNumberlist()))
                    {
                        outputMessages.Invalid("Length, Enter a 10 digit number");
                    }
                    outputMessages.Invalid("account number");
                }


                AccountManager.PrintBalance(user);
                outputMessages.Input("transfer amount");

                uint transferAmount = validator.LoopValidateFormat(uint.Parse, outputMessages);

                //confirm transfer succeessful then break
                if (user.Transfer(transferAmount, receivingUser))
                {
                    outputMessages.Success("Transfer");
                    AccountManager.PrintBalance(user);
                    break;
                }
                else
                {
                    outputMessages.Error("Transfer");
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

        outputMessages.Custom("\nwould you like to complete another transaction Y/N");
        string? response = Console.ReadLine();

        //come fix warning
        while (!validator.ValidateAgainstList<string>(response?.ToLower(), possibleEndOptions))
        {
            Console.WriteLine("invalid Input!, Try again");
            response = Console.ReadLine();
        }

        if (response?.ToLower() == "n" || response?.ToLower() == "no")
        {
            return;
        }

    }


    bool createAccount()
    {
        outputMessages.Input("new account number");
        uint inputtedAccountNumber = validator.LoopValidateFormat(uint.Parse, outputMessages);
        if (validator.ValidateAgainstList(inputtedAccountNumber, AccountManager.GetAccountNumberlist()))
        {
            outputMessages.DisplayValidationError("Account number, Account number exists already ");
            outputMessages.Error("Account Creation");
            return false;
        }
        else if (!validator.ValidateLength(inputtedAccountNumber, 10))
        {
            outputMessages.DisplayValidationError("Account number, account number is not 10 digits ");
            outputMessages.Error("Account Creation");
            return false;
        }

        outputMessages.Input("Balance, 0 for an Empty starting balance");
        uint inputtedBalance = validator.LoopValidateFormat(uint.Parse, outputMessages);


        outputMessages.Input("4 digit Pin");
        uint inputtedPin = validator.LoopValidateFormat(uint.Parse, outputMessages);
        if (!validator.ValidateLength(inputtedPin, 4))
        {
            outputMessages.DisplayValidationError("Pin, Enter a 4 digit pin");
            outputMessages.Error("Account Creation");
            return false;
        }

        AccountManager.Createuser(inputtedAccountNumber, inputtedBalance, inputtedPin);
        return true;

    }

}
