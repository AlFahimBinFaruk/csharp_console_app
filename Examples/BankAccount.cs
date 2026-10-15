namespace CSharpConsoleApp.Examples;

internal interface IDescribable
{
    string Describe();
}

internal sealed class BankAccount : IDescribable
{
    public string Owner { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal openingBalance)
    {
        Owner = owner;
        Balance = openingBalance;
    }

    public void Deposit(decimal amount) => Balance += amount;

    public string Describe() => $"{Owner}'s account balance is {Balance:C}";
}
