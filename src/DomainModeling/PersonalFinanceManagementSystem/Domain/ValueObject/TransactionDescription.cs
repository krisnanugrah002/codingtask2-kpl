namespace PersonalFinanceManagementSystem.Domain.ValueObject;

public class TransactionDescription
{
    public string Value { get; }

    public TransactionDescription(string description)
    {
        // Aturan Bisnis: Tidak boleh kosong atau spasi
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Deskripsi transaksi tidak boleh kosong.");

        Value = description;
    }
}
