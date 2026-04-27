namespace RetailInventorySystem.Domain.ValueObject;

public class ProductName
{
    public string Name { get; }

    public ProductName(string name)
    {
        // Aturan Bisnis: Tidak boleh kosong atau spasi
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nama produk tidak boleh kosong.");

        Name = name;
    }

    public override string ToString() => Name;
}
