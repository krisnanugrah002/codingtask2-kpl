using HotelReservationSystem.Domain.ValueObject;

namespace HotelReservationSystem.Domain.Entity;

public class Room
{
    public RoomType Type { get; private set; }
    public int Capacity { get; private set; }
    public int ReservedCount { get; private set; }

    public Room(RoomType type, int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Kapasitas kamar harus lebih dari nol.");

        Type = type;
        Capacity = capacity;
        ReservedCount = 0;
    }

    public bool IsAvailable() => ReservedCount < Capacity;

    public void Reserve()
    {
        // Aturan Bisnis: Cek ketersediaan sebelum reservasi
        if (!IsAvailable())
            throw new InvalidOperationException($"Kamar tipe {Type} sudah penuh.");

        ReservedCount++;
    }

    public void Release()
    {
        if (ReservedCount > 0)
            ReservedCount--;
    }
}
