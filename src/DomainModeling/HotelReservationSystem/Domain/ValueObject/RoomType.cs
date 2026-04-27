namespace HotelReservationSystem.Domain.ValueObject;

public class RoomType
{
    public string Type { get; }
    private static readonly string[] AllowedTypes = { "Standard", "Deluxe", "Suite" };

    public RoomType(string type)
    {
        // Aturan Bisnis: Tipe kamar harus valid
        if (!AllowedTypes.Contains(type))
            throw new ArgumentException($"Tipe kamar tidak valid. Pilihan: {string.Join(", ", AllowedTypes)}");

        Type = type;
    }

    public override string ToString() => Type;
}
