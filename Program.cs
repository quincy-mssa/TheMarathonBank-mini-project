using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheMarathonBank__mini_project
{
    internal class Program
    {
        //path to the file that stores account data
        private const string DatabaseFile = @"C:\Users\quinc\OneDrive\Desktop\The Marathon Accounts.txt";

        static void Main(string[] args)
        {
            //main menu loop for the banking application
            while (true)
            {
                // show the app menu options to the user
                Console.WriteLine("\nWelcome to The Marathon Banking App!");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. View Balance");
                Console.WriteLine("5. Exit");

                //ask user to choose an option
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                //choose the action based on user's choice
                switch (choice)
                {
                    case "1":
                        CreateAccount(); //creates a new account
                        break;
                    case "2":
                        Deposit(); //makes a deposit
                        break;
                    case "3":
                        Withdraw(); //makes a withdrawal
                        break;
                    case "4":
                        ViewBalance(); //view account balance
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using The Marathon Bank App!");
                        return; //exit application
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        //
        //create a new account with a unique account number
        private static void CreateAccount()
        {
            Console.Write("Enter account holder's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter initial deposit amount: ");

            // validate deposit amount
            if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) && initialDeposit >= 0)
            {
                //get the next available account number
                int nextAccountNumber = GetNextAccountNumber();
                var account = new Account(nextAccountNumber, name, initialDeposit);

                //save the new account to the database
                SaveAccount(account);
                Console.WriteLine($"Account created successfully! Account Number: {account.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount. Account creation failed.");
            }
        }

        //deposit money into an account
        private static void Deposit()
        {
            Console.Write("Enter account number: ");

            //validate account number input
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                //load accounts from the database
                var accounts = LoadAccounts();
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (account != null)
                {
                    Console.Write("Enter deposit amount: ");

                    //validate deposit amount
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                    {
                        account.Deposit(amount); //perform deposit
                        SaveAccounts(accounts); //save updated accounts to the database
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number.");
            }
        }

        //withdraw funds from an account
        private static void Withdraw()
        {
            Console.Write("Enter account number: ");

            //validate account number input
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                //load all accounts from the database
                var accounts = LoadAccounts();
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (account != null)
                {
                    Console.Write("Enter withdrawal amount: ");

                    //validate withdrawal amount and check funds
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0 && amount <= account.Balance)
                    {
                        account.Withdraw(amount); //perform withdrawal
                        SaveAccounts(accounts); //save updated accounts to the database
                    }
                    else
                    {
                        Console.WriteLine("Invalid withdrawal amount or insufficient funds.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number.");
            }
        }

        //view the balance of a specific account
        private static void ViewBalance()
        {
            Console.Write("Enter account number: ");

            //validate account number input
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                //load all accounts from the database
                var accounts = LoadAccounts();
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (account != null)
                {
                    account.DisplayBalance(); //display the account balance
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number.");
            }
        }

        //loadaccounts from the database file
        private static List<Account> LoadAccounts()
        {
            var accounts = new List<Account>();
            if (File.Exists(DatabaseFile)) //check if the file exists
            {
                var lines = File.ReadAllLines(DatabaseFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(','); //split each line into components
                    if (parts.Length == 3 &&
                        int.TryParse(parts[0], out int accountNumber) &&
                        decimal.TryParse(parts[2], out decimal balance))
                    {
                        accounts.Add(new Account(accountNumber, parts[1], balance)); //add account to the list
                    }
                }
            }
            return accounts;
        }

        //save a new account to the database file
        private static void SaveAccount(Account account)
        {
            using var writer = new StreamWriter(DatabaseFile, append: true);
            writer.WriteLine($"{account.AccountNumber},{account.AccountHolderName},{account.Balance}");
        }

        //save all accounts to the database file
        private static void SaveAccounts(List<Account> accounts)
        {
            using var writer = new StreamWriter(DatabaseFile, append: false);
            foreach (var account in accounts)
            {
                writer.WriteLine($"{account.AccountNumber},{account.AccountHolderName},{account.Balance}");
            }
        }

        //get the next available account number
        private static int GetNextAccountNumber()
        {
            var accounts = LoadAccounts();
            return accounts.Count > 0 ? accounts.Max(a => a.AccountNumber) + 1 : 1;
        }
    }
}
