namespace RetailInventorySystem.Domain.ValueObject;

public class Discount
{
    public int Percentage { get; }

    public Discount(int percentage)
    {
        // Aturan Bisnis: 0-100%
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Diskon harus antara 0 hingga 100 persen.");

        Percentage = percentage;
    }

    public decimal Apply(decimal originalPrice) => originalPrice * (1 - (decimal)Percentage / 100);
}
