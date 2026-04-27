namespace HotelReservationSystem.Domain.ValueObject;

public class GuestName
{
    public string Name { get; }

    public GuestName(string name)
    {
        // Aturan Bisnis: Nama tidak boleh kosong atau hanya spasi
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nama tamu tidak boleh kosong.");

        Name = name;
    }

    public override string ToString() => Name;
}
