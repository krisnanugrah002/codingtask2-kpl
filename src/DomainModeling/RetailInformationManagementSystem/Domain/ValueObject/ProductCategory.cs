namespace RetailInventorySystem.Domain.ValueObject;

public class ProductCategory
{
    public string Category { get; }
    private static readonly string[] AllowedCategories = { "Electronics", "Groceries", "Clothing", "Furniture" };

    public ProductCategory(string category)
    {
        // Aturan Bisnis: Kategori yang diizinkan
        if (!AllowedCategories.Contains(category))
            throw new ArgumentException($"Kategori tidak valid. Pilihan: {string.Join(", ", AllowedCategories)}");

        Category = category;
    }

    public override string ToString() => Category;
}

