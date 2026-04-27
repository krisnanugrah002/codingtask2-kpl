using HotelReservationSystem.Domain.ValueObject;

namespace HotelReservationSystem.Domain.Entity;

public class Reservation
{
    public Guid Id { get; private set; }
    public RoomType RoomType { get; private set; }
    public GuestName GuestName { get; private set; }
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    public bool IsCancelled { get; private set; }

    public Reservation(RoomType roomType, GuestName guestName, DateTime checkIn, DateTime checkOut)
    {
        // Aturan Bisnis: Tanggal check-in tidak boleh di masa lalu
        if (checkIn.Date < DateTime.Now.Date)
            throw new ArgumentException("Tanggal check-in tidak boleh di masa lalu.");

        // PERBAIKAN: Menggunakan .Date untuk perbandingan.
        // Ini memastikan jika check-in dan check-out dilakukan di hari yang sama (seperti di Unit Test), 
        // maka akan tetap melempar ArgumentException meskipun ada selisih milidetik dari DateTime.Now.
        if (checkOut.Date <= checkIn.Date)
            throw new ArgumentException("Tanggal check-out harus setelah tanggal check-in.");

        Id = Guid.NewGuid();
        RoomType = roomType;
        GuestName = guestName;
        CheckInDate = checkIn;
        CheckOutDate = checkOut;
        IsCancelled = false;
    }

    public void ModifyReservation(DateTime newCheckIn, DateTime newCheckOut)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Tidak bisa mengubah reservasi yang sudah dibatalkan.");

        if (DateTime.Now >= CheckInDate)
            throw new InvalidOperationException("Modifikasi hanya bisa dilakukan sebelum periode reservasi dimulai.");

        if (newCheckIn.Date < DateTime.Now.Date)
            throw new ArgumentException("Tanggal check-in baru tidak boleh di masa lalu.");

        // Konsistensi menggunakan .Date untuk modifikasi
        if (newCheckOut.Date <= newCheckIn.Date)
            throw new ArgumentException("Tanggal check-out harus setelah tanggal check-in.");

        CheckInDate = newCheckIn;
        CheckOutDate = newCheckOut;
    }

    public void CancelReservation()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Reservasi sudah dibatalkan sebelumnya.");

        if (DateTime.Now >= CheckInDate)
            throw new InvalidOperationException("Pembatalan hanya bisa dilakukan sebelum periode reservasi dimulai.");

        IsCancelled = true;
    }

    public override string ToString() => $"Reservation {Id}: {GuestName} ({RoomType}) | {CheckInDate:dd/MM/yyyy} - {CheckOutDate:dd/MM/yyyy}";
}
