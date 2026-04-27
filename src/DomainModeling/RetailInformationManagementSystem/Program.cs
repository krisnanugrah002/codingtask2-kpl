using RetailInventorySystem.Domain.Entity;
using RetailInventorySystem.Domain.ValueObject;

namespace RetailInventorySystem;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== RETAIL INVENTORY SYSTEM ===");

            // Membuat produk valid
            var laptop = new Product(
                new ProductName("Laptop Gaming"),
                new ProductCategory("Electronics"),
                new Price(2000.00m),
                new StockQuantity(50)
            );

            laptop.SetDiscount(new Discount(10));
            Console.WriteLine($"Product: {laptop}, Final Price: {laptop.GetFinalPrice():C}");

            // Mengelola stok produk
            laptop.AddStock(20);
            laptop.ReduceStock(30);
            Console.WriteLine($"After Stock Update: {laptop}");

            // Membuat bundel produk
            var mouse = new Product(
                new ProductName("Wireless Mouse"),
                new ProductCategory("Electronics"),
                new Price(50.00m),
                new StockQuantity(100)
            );

            var bundle = new ProductBundle("Laptop + Mouse Bundle");
            bundle.AddProduct(laptop);
            bundle.AddProduct(mouse);
            bundle.SetBundlePrice(1900.00m);
            Console.WriteLine(bundle);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected Error: " + ex.Message);
        }
    }
}

