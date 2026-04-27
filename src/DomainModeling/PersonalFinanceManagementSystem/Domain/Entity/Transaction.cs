using PersonalFinanceManagementSystem.Domain.ValueObject;

namespace PersonalFinanceManagementSystem.Domain.Entity;

// Entity for Transaction
public class Transaction
{
    public Amount Amount { get; }
    public TransactionCategory Category { get; }
    public TransactionDate Date { get; }
    public TransactionDescription Description { get; }

    public Transaction(Amount amount, TransactionCategory category, TransactionDate date, TransactionDescription description)
    {
        Amount = amount;
        Category = category;
        Date = date;
        Description = description;
    }
}

