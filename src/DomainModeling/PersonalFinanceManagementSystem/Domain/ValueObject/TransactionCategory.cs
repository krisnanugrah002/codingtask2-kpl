namespace PersonalFinanceManagementSystem.Domain.ValueObject;

public class TransactionCategory
{
    public string Value { get; }
    private static readonly string[] AllowedCategories = { "Income", "Expense", "Savings", "Investment" };

    public TransactionCategory(string category)
    {
        // Aturan Bisnis: Kategori harus dalam daftar yang diizinkan
        if (!AllowedCategories.Contains(category))
            throw new ArgumentException($"Kategori tidak valid. Pilihan: {string.Join(", ", AllowedCategories)}");

        Value = category;
    }
}
