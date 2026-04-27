using PersonalFinanceManagementSystem.Domain.ValueObject;

namespace PersonalFinanceManagementSystem.Domain.Entity;

public class Account
{
    public string Name { get; private set; }
    public Amount Balance { get; private set; }
    private readonly List<Transaction> _transactions = new();

    public Account(string name, Amount initialBalance)
    {
        Name = name;
        Balance = initialBalance;
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.Category.Value == "Expense")
        {
            // Aturan Bisnis: Saldo tidak boleh negatif setelah transaksi dilakukan
            if (Balance.Value < transaction.Amount.Value)
                throw new InvalidOperationException("Saldo tidak mencukupi untuk transaksi pengeluaran ini.");

            Balance = new Amount(Balance.Value - transaction.Amount.Value);
        }
        else if (transaction.Category.Value == "Income")
        {
            Balance = new Amount(Balance.Value + transaction.Amount.Value);
        }

        _transactions.Add(transaction);
    }

    public override string ToString() => $"Account: {Name} | Current Balance: {Balance.Value:C}";
}
