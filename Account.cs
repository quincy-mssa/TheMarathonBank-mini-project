using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarathonBank__mini_project
{
    public class Account
    {
        //Account class properties
        public int AccountNumber { get; set; } // Unique identifier for each account
        public string AccountHolderName { get; set; } // Name of the account holder
        public decimal Balance { get; private set; } // Balance amount

        //takes the provided values and assigns them to the class properties (AccountNumber, AccountHolderName, and Balance).
        public Account(int accountNumber, string accountHolderName, decimal initialDeposit)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Balance = initialDeposit;
        }


        //Deposit method
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Successfully deposited {amount:C}. New balance is {Balance:C}.");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        //Withdraw method
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Successfully withdrew {amount:C}. New balance is {Balance:C}.");
            }
            else if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
            }
            else
            {
                Console.WriteLine("Withdrawal amount must be positive.");
            }
        }

        //Display balance method
        public void DisplayBalance()
        {
            Console.WriteLine($"Account Balance for {AccountHolderName}: {Balance:C}");
        }
    }
}
