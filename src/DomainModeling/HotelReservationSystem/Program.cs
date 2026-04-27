using HotelReservationSystem.Domain.Entity;
using HotelReservationSystem.Domain.ValueObject;

namespace HotelReservationSystem;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== HOTEL RESERVATION SYSTEM ===");

            // Membuat reservasi valid
            var reservation = new Reservation(
                new RoomType("Deluxe"),
                new GuestName("Jane Doe"),
                DateTime.Now.AddDays(2),
                DateTime.Now.AddDays(7)
            );
            Console.WriteLine("Reservation Created: " + reservation);

            // Mengubah reservasi sebelum periode reservasi dimulai
            reservation.ModifyReservation(DateTime.Now.AddDays(3), DateTime.Now.AddDays(8));
            Console.WriteLine("Reservation Modified: " + reservation);

            // Membatalkan reservasi
            reservation.CancelReservation();
            Console.WriteLine("Reservation Canceled");

            // Mengecek ketersediaan kamar
            var room = new Room(new RoomType("Deluxe"), 5);
            Console.WriteLine("Room Available: " + room.IsAvailable());

            room.Reserve();
            Console.WriteLine("Room Available After Reservation: " + room.IsAvailable());

        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected Error: " + ex.Message);
        }
    }
}
