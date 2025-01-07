public class Account //Account class represents the bank accounts with basic transactions such as deposit, withdrawal, and balance display.

{
    //set numerical identifier for the account.
    public int AccountNumber { get; set; }

    //name of the account holder.
    public string AccountHolderName { get; set; }

    //current balance of the account. 
    public decimal Balance { get; private set; }// The setter is private to ensure that balance can only be modified via defined methods (e.g., Deposit, Withdraw).


    // Constructor to initialize a new account with appropriate data
    public Account(int accountNumber, string accountHolderName, decimal initialDeposit)
    {
        AccountNumber = accountNumber; //assign the account number.
        AccountHolderName = accountHolderName; //assign the account holder's name.
        Balance = initialDeposit; //set the initial balance.
    }

    //deposit method
    public void Deposit(decimal amount)
    {
        Balance += amount; // Add the deposit amount to the balance.
        Console.WriteLine($"Successfully deposited {amount:C}. New balance is {Balance:C}.");
    }

    //withdrawal method
    public void Withdraw(decimal amount)
    {
        Balance -= amount; // Deduct the withdrawal amount from the balance.
        Console.WriteLine($"Successfully withdrew {amount:C}. New balance is {Balance:C}.");
    }

    //display balance method
    public void DisplayBalance()
    {
        Console.WriteLine($"Account Balance for {AccountHolderName}: {Balance:C}");
    }
}
