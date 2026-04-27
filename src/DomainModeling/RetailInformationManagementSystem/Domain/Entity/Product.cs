using RetailInventorySystem.Domain.ValueObject;

namespace RetailInventorySystem.Domain.Entity;

public class Product
{
    public ProductName Name { get; private set; }
    public ProductCategory Category { get; private set; }

    // Mengubah 'BasePrice' menjadi 'Price' agar sesuai dengan InventoryTests.cs line 86
    public Price Price { get; private set; }
    public StockQuantity Stock { get; private set; }
    public Discount Discount { get; private set; }

    public Product(ProductName name, ProductCategory category, Price price, StockQuantity stock)
    {
        Name = name;
        Category = category;
        Price = price;
        Stock = stock;
        Discount = new Discount(0);
    }

    public void AddStock(int amount) => Stock = new StockQuantity(Stock.Quantity + amount);

    public void ReduceStock(int amount)
    {
        if (amount > Stock.Quantity)
            throw new InvalidOperationException("Stok tidak cukup untuk dikurangi.");

        Stock = new StockQuantity(Stock.Quantity - amount);
    }

    public void SetDiscount(Discount discount) => Discount = discount;

    // Menggunakan properti Amount dari objek Price yang sudah diperbarui
    public decimal GetFinalPrice() => Discount.Apply(Price.Amount);

    public override string ToString() => $"{Name} ({Category}) - Price: {Price.Amount:C}, Stock: {Stock.Quantity}";
}
