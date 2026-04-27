namespace RetailInventorySystem.Domain.ValueObject;

public class Price
{
    // Mengubah 'Value' menjadi 'Amount' agar sesuai dengan InventoryTests.cs line 41
    public decimal Amount { get; }

    public Price(decimal amount)
    {
        // Aturan Bisnis: Harga tidak boleh negatif atau nol
        if (amount <= 0)
            throw new ArgumentException("Harga produk harus lebih dari nol.");

        Amount = amount;
    }
}
