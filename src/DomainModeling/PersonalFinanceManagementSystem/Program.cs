using PersonalFinanceManagementSystem.Domain.Entity;
using PersonalFinanceManagementSystem.Domain.ValueObject;

namespace PersonalFinanceManagementSystem;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== PERSONAL FINANCE MANAGEMENT SYSTEM ===");

            var account = new Account("Personal Savings", new Amount(1000m));
            Console.WriteLine(account);

            // Test Income Transaction
            var transaction1 = new Transaction(
                new Amount(500m),
                new TransactionCategory("Income"),
                new TransactionDate(DateTime.Now),
                new TransactionDescription("Freelance Project")
            );
            account.AddTransaction(transaction1);
            Console.WriteLine("After Income: " + account);

            // Test Expense Transaction
            var transaction2 = new Transaction(
                new Amount(200m),
                new TransactionCategory("Expense"),
                new TransactionDate(DateTime.Now),
                new TransactionDescription("Groceries")
            );
            account.AddTransaction(transaction2);
            Console.WriteLine("After Expense: " + account);

            // Test Insufficient Balance (should throw exception)
            Console.WriteLine("\nTesting Insufficient Balance (Expense 2000)...");
            var transaction3 = new Transaction(
                new Amount(2000m),
                new TransactionCategory("Expense"),
                new TransactionDate(DateTime.Now),
                new TransactionDescription("Laptop Purchase")
            );
            account.AddTransaction(transaction3);
            Console.WriteLine(account);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
