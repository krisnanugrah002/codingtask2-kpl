namespace PersonalFinanceManagementSystem.Domain.ValueObject;

public class TransactionDate
{
    public DateTime Value { get; }

    public TransactionDate(DateTime date)
    {
        // Aturan Bisnis: Tidak boleh lebih dari hari ini
        if (date.Date > DateTime.Now.Date)
            throw new ArgumentException("Tanggal transaksi tidak boleh lebih dari hari ini.");

        Value = date;
    }
}
