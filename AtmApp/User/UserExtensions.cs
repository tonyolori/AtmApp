using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp.User
{
    public static class UserExtensions
    {
        static IMessages messages = Factory.CreateMessages();
        private static IInputValidator Validator = Factory.CreateValidator();
        public static void Deposit(this UserAccount user, uint amount)
        {
            user.Balance += amount;
            AccountManager.Update(user);
            return;
        }

        public static void Withdraw(this UserAccount user, uint amount)
        {
            
            if (user.Balance - amount >= 0)
            {
                user.Balance -= amount;
                AccountManager.Update(user);
            }

            else
            {
                throw new Exception("Insufficient balance");
            }

        }


        public static bool Transfer(this UserAccount user, uint amount, UserAccount receiver)
        {
            //if withdrawal is successful then deposit into the new account   
            try
            {
                user.Withdraw(amount);
                receiver.Deposit(amount);
                AccountManager.Update(user);
                AccountManager.Update(receiver);
                return true;
            }
            catch
            {
                messages.Error("Transaction");
            }

            return false;
        }

        public static bool MatchPin(this UserAccount user, uint input)
        {
            if (input == user.Pin)
            {
                return true;
            }

            return false;
        }
        public static void ChangePin(this UserAccount user, uint newPin)
        {
            user.Pin = newPin;
            AccountManager.Update(user);
        }


        public static UserAccount Login(this UserAccount user)
        {
            int MaxLoginAttempts = 4;

            messages.Input("pin");

            while (MaxLoginAttempts > 0)
            {
                
                if (!Validator.ValidateFormat(uint.Parse, Console.ReadLine(), out uint entryPin))
                {
                    MaxLoginAttempts--;
                    messages.DisplayValidationError("Pin format. {MaxLoginAttempts} attempts remaining");
                    continue;
                }

                if (user.MatchPin(entryPin))
                {
                    messages.Success("Login");
                    return user;
                }

                MaxLoginAttempts--;
                messages.Invalid($"Pin. {MaxLoginAttempts} attempts remaining");
            }

            messages.Custom("Maximum login attempts exceeded. Card swallowed!");
            return null; // or throw an exception indicating maximum attempts exceeded
        }
    }
}
