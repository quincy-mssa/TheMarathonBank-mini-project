namespace TheMarathonBank__mini_project
{
    //This is the Marathong Banking Application. This banking application is the foundation of a future banking application
    //that will contain a fully functional GUI.
    internal class Program
    {
        //List to store bank accounts
        private static List<Account> accounts = new List<Account>();

        //counter to assign a unique account number to each new account created
        private static int nextAccountNumber = 1;

        static void Main(string[] args)
        {

            while (true)
            {
                //Prompts to display for menu choices
                Console.WriteLine("\nWelcome to The Marathon Banking App!");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. View Balance");
                Console.WriteLine("5. Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": //creates a new account
                        CreateAccount();
                        break;
                    case "2": //starts a deposit
                        Deposit();
                        break;
                    case "3": //starts a withdrawal
                        Withdraw();
                        break;
                    case "4": //displays account balance
                        ViewBalance();
                        break;
                    case "5": //exits application
                        Console.WriteLine("Thank you for using the Banking App. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        //creates a new Account object and adds it to the accounts list
        private static void CreateAccount()
        {
            Console.Write("Enter account holder's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter initial deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) && initialDeposit >= 0)
            {
                var account = new Account(nextAccountNumber++, name, initialDeposit);
                accounts.Add(account);
                Console.WriteLine($"Account created successfully! Account Number: {account.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount. Account creation failed.");
            }
        }

        //method to create a deposit
        private static void Deposit()
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    Console.Write("Enter deposit amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        account.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
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

        //method for withdrawal
        private static void Withdraw()
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    Console.Write("Enter withdrawal amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        account.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
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

        //method to view remaining balance
        private static void ViewBalance()
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.DisplayBalance();
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
    }
}
