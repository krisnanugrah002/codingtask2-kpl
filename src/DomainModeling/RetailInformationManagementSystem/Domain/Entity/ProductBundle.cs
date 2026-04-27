using System.Globalization;

namespace RetailInventorySystem.Domain.Entity;

public class ProductBundle
{
    public string BundleName { get; private set; }
    private readonly List<Product> _products = new();
    public decimal BundlePrice { get; private set; }

    public ProductBundle(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nama bundel tidak boleh kosong.");
        BundleName = name;
    }

    public void AddProduct(Product product) => _products.Add(product);

    public void SetBundlePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Harga bundel harus positif.");
        BundlePrice = price;
    }

    // Perbaikan format ToString() agar sesuai dengan ekspektasi InventoryTests.cs line 117
    // Menggunakan InvariantCulture agar menghasilkan format $1,900.00
    public override string ToString() =>
        $"{BundleName} - Price: {BundlePrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"))}, Items: {_products.Count}";

}
