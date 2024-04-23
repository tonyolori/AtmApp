using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp
{
    class UserAccount
    {
        private uint accountNumber;
        private int balance;
        private uint pin;

        //what would be a better variable name?
        public UserAccount(uint uAccountNumber, int ubalance, uint upin)
        {
            balance = ubalance;
            pin = upin;
            accountNumber = uAccountNumber;
        }

        public bool Deposit(uint amount)
        {
            balance += (int)amount;
            return true;
        }

        public bool Withdraw(uint amount)
        {
            if (balance - amount >= 0)
            {
                balance -= (int)amount;
                return true;
            }

            else
            { 
                return false;
            }

        }

        public int GetBalance()
        {
            return balance;
        }

        public uint GetAccountNumber()
        {
            return accountNumber;
        }

        public bool Transfer(uint amount, UserAccount receiver)
        { 
            //if withdrawal is successful then deposit into the new account   
            if (Withdraw(amount)) 
            { 
            receiver.Deposit(amount);
                return true;
            }

            return false;
        } 

        public bool MatchPin(uint input)
        {
            if (input == pin)
            {             
                return true;
            }

            else return false;
        }
        public void ChangePin(uint newPin)
        {
            pin = newPin;
        }

        //converts a list of <UserAccount> to a list of uint
        public static List<uint> GetAccountNumberlist(List<UserAccount> accounts)
        {
            List<uint> accountList = new();

            foreach(var account in accounts)
            {
            accountList.Add(account.GetAccountNumber());
            }

            return accountList;
        }
        
    }
}
