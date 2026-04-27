namespace PersonalFinanceManagementSystem.Domain.ValueObject;

public class Amount
{
    public decimal Value { get; }

    public Amount(decimal value)
    {
        // Aturan Bisnis: Tidak boleh negatif
        if (value < 0)
            throw new ArgumentException("Jumlah (Amount) tidak boleh negatif.");

        Value = value;
    }
}
