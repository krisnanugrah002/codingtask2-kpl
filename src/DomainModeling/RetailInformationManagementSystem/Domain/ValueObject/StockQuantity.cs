namespace RetailInventorySystem.Domain.ValueObject;

public class StockQuantity
{
    public int Quantity { get; }

    public StockQuantity(int quantity)
    {
        // Aturan Bisnis: Tidak boleh negatif
        if (quantity < 0)
            throw new ArgumentException("Jumlah stok tidak boleh negatif.");

        Quantity = quantity;
    }
}
