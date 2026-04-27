[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/0kAYeE_7)
# domain-modeling-csharp

### **Learning Objectives:**

* Understand the basic concepts of **Value Objects** and **Entities** in object-oriented design.
* Understand how to separate domain logic from application logic.
* Be able to apply DDD (Domain-Driven Design) principles such as **Encapsulation**, **Invariants**, and **Business Rules**.
* Be able to create and manage **Unit Tests** to ensure code quality.
* Be able to utilize **MSTest** for test automation.

---

### Assignment 1: Personal Finance Management System

**Case Description:**
This system is used to manage personal finances, record expenses, income, and generate monthly financial reports. Each transaction has several components, including category, amount, date, and description.

**Classes Involved:**

* **Entity:** Transaction, Account
* **Value Objects:** Amount, TransactionCategory, TransactionDate, TransactionDescription

**Implementation Details:**

1. **Amount (Value Object)**

   * Stores the transaction amount.
   * Must not be negative.
   * Must use the `decimal` data type to avoid precision issues.
   * Example: `new Amount(200.50m)`

2. **TransactionCategory (Value Object)**

   * Stores the transaction category.
   * Allowed categories: `Income`, `Expense`, `Savings`, `Investment`.
   * Any category outside this list must produce an error.
   * Example: `new TransactionCategory("Income")`

3. **TransactionDate (Value Object)**

   * Stores the transaction date.
   * Must not be later than today (current date).
   * Must use the `DateTime` data type.
   * Example: `new TransactionDate(DateTime.Now)`

4. **TransactionDescription (Value Object)**

   * Stores the transaction description.
   * Must not be empty or contain only whitespace.
   * Example: `new TransactionDescription("Freelance Project")`

5. **Transaction (Entity)**

   * Uses `Amount`, `TransactionCategory`, `TransactionDate`, and `TransactionDescription` as its attributes.
   * Example:

     ```csharp
     new Transaction(new Amount(200), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Freelance Project"));
     ```

6. **Account (Entity)**

   * Stores the account balance.
   * Can add transactions if the balance is sufficient for the `Expense` category.
   * The balance must not be negative after a transaction is applied.
   * Example:

     ```csharp
     var account = new Account("Personal Savings", new Amount(1000));
     account.AddTransaction(new Transaction(new Amount(200), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Salary")));
     ```

**Business Rules:**

1. A transaction cannot be created if the amount (`Amount`) is negative.
2. The transaction date (`TransactionDate`) must not be later than today.
3. The transaction category (`TransactionCategory`) must be within the allowed categories (`Income`, `Expense`, `Savings`, `Investment`).
4. The transaction description (`TransactionDescription`) must not be empty.
5. The total account balance (`Account`) must not be negative.
6. Transactions can be categorized as `Income` or `Expense`.

**Testing:**
Students must ensure that each business rule above is properly implemented in unit tests.

**Program.cs for Manual Testing:**
```csharp
using System;

namespace PersonalFinanceManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var account = new Account("Personal Savings", new Amount(1000));
                Console.WriteLine(account);

                // Test Income Transaction
                var transaction1 = new Transaction(new Amount(500), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Freelance Project"));
                account.AddTransaction(transaction1);
                Console.WriteLine(account);

                // Test Expense Transaction
                var transaction2 = new Transaction(new Amount(200), new TransactionCategory("Expense"), new TransactionDate(DateTime.Now), new TransactionDescription("Groceries"));
                account.AddTransaction(transaction2);
                Console.WriteLine(account);

                // Test Insufficient Balance (should throw exception)
                var transaction3 = new Transaction(new Amount(2000), new TransactionCategory("Expense"), new TransactionDate(DateTime.Now), new TransactionDescription("Laptop Purchase"));
                account.AddTransaction(transaction3);
                Console.WriteLine(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
```

---

### Assignment 2: Hotel Reservation System

**Case Description:**
This system is used to manage hotel room reservations. Each reservation contains information about the guest, stay period, room type, and total cost. The system must ensure that every reservation is valid and complies with the applicable business rules.

**Classes Involved:**

* **Entity:** Reservation, Guest, Room
* **Value Objects:** RoomType, GuestName, GuestContact, ReservationPeriod, ReservationAmount

**Implementation Details:**

1. **RoomType (Value Object)**

   * Stores a valid room type.
   * Allowed types: `Standard`, `Deluxe`, `Suite`.
   * Any other type must produce an error.
   * Example: `new RoomType("Standard")`

2. **GuestName (Value Object)**

   * Stores the guest name.
   * Must not be empty or contain only whitespace.
   * Example: `new GuestName("John Doe")`

3. **Reservation (Entity)**

   * Stores reservation information including room type, guest name, check-in date, and check-out date.
   * The check-in date must not be in the past.
   * The check-out date must be after the check-in date.
   * Can be modified using `ModifyReservation()` before the reservation period starts.
   * Can be canceled using `CancelReservation()` before the reservation period starts.

4. **Room (Entity)**

   * Stores room information including room type and capacity.
   * Checks room availability before reservation.
   * Decreases capacity when reserved and restores it when canceled.

**Business Rules:**

1. The reservation period (`ReservationPeriod`) must not be in the past.
2. A reservation cannot be created if the desired room (`RoomType`) is fully booked.
3. The reservation cost (`ReservationAmount`) must match the room rate and stay period.
4. The room type (`RoomType`) must be valid (`Standard`, `Deluxe`, `Suite`).
5. Reservation modifications can only be made before the reservation period starts.
6. The guest name (`GuestName`) must not be empty.
7. The guest contact (`GuestContact`) must be a valid phone number or email.
8. Reservations can be canceled before the reservation period starts, and room capacity must be restored.

**Program.cs for Manual Testing:**
```csharp
using System;

namespace HotelReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Membuat reservasi valid
                var reservation = new Reservation(new RoomType("Deluxe"), new GuestName("Jane Doe"), DateTime.Now.AddDays(2), DateTime.Now.AddDays(7));
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
}
```

---

### Assignment 3: Retail Inventory Management System

**Case Description:**
This system is used to manage store product inventory, including stock, pricing, and product categories. The system must ensure that every product has complete and valid details and allows safe stock management. Additionally, the system must support discount features, bundling, and expired product handling.

**Classes Involved:**

* **Entity:** Product, ProductBundle
* **Value Objects:** ProductName, ProductCategory, Price, StockQuantity, ExpiryDate, Discount

**Implementation Details:**

1. **ProductName (Value Object)**

   * Stores the product name.
   * Must not be empty or contain only whitespace.

2. **ProductCategory (Value Object)**

   * Stores a valid product category.
   * Allowed categories: `Electronics`, `Groceries`, `Clothing`, `Furniture`.
   * Any other category must produce an error.

3. **Price (Value Object)**

   * Stores the product price.
   * Must not be negative or zero.
   * Uses `decimal` to avoid precision issues.

4. **StockQuantity (Value Object)**

   * Stores product stock quantity.
   * Must not be negative.

5. **ExpiryDate (Value Object)**

   * Stores expiration date for perishable products.
   * Must not be in the past at creation time.

6. **Discount (Value Object)**

   * Stores discount percentage (`0–100%`).
   * Must not be negative or exceed 100%.

7. **Product (Entity)**

   * Stores product details including name, category, price, stock, discount, and expiration date.
   * Can add/reduce stock.
   * Can apply discounts and calculate final price.
   * Can mark a product as expired.

8. **ProductBundle (Entity)**

   * Stores bundled products with a special price.
   * Manages stock for all included products.

**Program.cs for Manual Testing:**
```csharp
using System;

namespace RetailInventorySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Membuat produk valid
                var laptop = new Product(new ProductName("Laptop"), new ProductCategory("Electronics"), new Price(2000.00m), new StockQuantity(50));
                laptop.SetDiscount(new Discount(10));
                Console.WriteLine($"Product: {laptop}, Final Price: {laptop.GetFinalPrice()}");

                // Mengelola stok produk
                laptop.AddStock(20);
                laptop.ReduceStock(30);
                Console.WriteLine($"After Stock Update: {laptop}");

                // Membuat bundel produk
                var mouse = new Product(new ProductName("Mouse"), new ProductCategory("Electronics"), new Price(50.00m), new StockQuantity(100));
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
}
```

---



---

# domain-modeling-csharp

### **Tujuan Pembelajaran:**

* Memahami konsep dasar **Value Objects** dan **Entities** dalam desain berorientasi objek.
* Memahami cara memisahkan logika domain dari logika aplikasi.
* Mampu menerapkan prinsip-prinsip desain DDD (Domain-Driven Design) seperti **Encapsulation**, **Invariants**, dan **Business Rules**.
* Mampu membuat dan mengelola **Unit Tests** untuk memastikan kualitas kode.
* Mampu memanfaatkan **MSTest** untuk otomatisasi pengujian.

---

### Tugas 1: Sistem Manajemen Keuangan Pribadi (Personal Finance Management System)

**Deskripsi Kasus:**  
Sistem ini digunakan untuk mengelola keuangan pribadi, mencatat pengeluaran, pemasukan, dan menghasilkan laporan keuangan bulanan. Setiap transaksi memiliki beberapa komponen, termasuk kategori, jumlah, tanggal, dan deskripsi.

**Kelas yang Terlibat:**  
- **Entity:** Transaction, Account  
- **Value Objects:** Amount, TransactionCategory, TransactionDate, TransactionDescription  

**Detail Implementasi:**
1. **Amount (Value Object)**  
   - Menyimpan jumlah transaksi.
   - Tidak boleh negatif.
   - Harus menggunakan tipe data `decimal` untuk menghindari masalah presisi.
   - Contoh: `new Amount(200.50m)`  

2. **TransactionCategory (Value Object)**  
   - Menyimpan kategori transaksi.
   - Kategori yang diizinkan: `Income`, `Expense`, `Savings`, `Investment`.  
   - Kategori lain selain ini harus menghasilkan error.
   - Contoh: `new TransactionCategory("Income")`  

3. **TransactionDate (Value Object)**  
   - Menyimpan tanggal transaksi.
   - Tidak boleh lebih dari hari ini (tanggal saat ini).
   - Harus menggunakan tipe data `DateTime`.
   - Contoh: `new TransactionDate(DateTime.Now)`  

4. **TransactionDescription (Value Object)**  
   - Menyimpan deskripsi transaksi.
   - Tidak boleh kosong atau hanya berisi spasi.
   - Contoh: `new TransactionDescription("Freelance Project")`  

5. **Transaction (Entity)**  
   - Menggunakan `Amount`, `TransactionCategory`, `TransactionDate`, dan `TransactionDescription` sebagai atributnya.
   - Contoh:  
     ```csharp
     new Transaction(new Amount(200), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Freelance Project"));
     ```  

6. **Account (Entity)**  
   - Menyimpan saldo akun.
   - Bisa menambah transaksi jika saldo mencukupi untuk kategori `Expense`.
   - Saldo tidak boleh negatif setelah transaksi dilakukan.
   - Contoh:  
     ```csharp
     var account = new Account("Personal Savings", new Amount(1000));
     account.AddTransaction(new Transaction(new Amount(200), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Salary")));
     ```  

**Business Rules:**  
1. Transaksi tidak dapat dibuat jika jumlah (`Amount`) negatif.  
2. Tanggal transaksi (`TransactionDate`) tidak boleh lebih dari hari ini.  
3. Kategori transaksi (`TransactionCategory`) harus termasuk dalam daftar kategori yang diizinkan (`Income`, `Expense`, `Savings`, `Investment`).  
4. Deskripsi transaksi (`TransactionDescription`) tidak boleh kosong.  
5. Total saldo pada akun (`Account`) tidak boleh negatif.  
6. Transaksi dapat dikategorikan sebagai `Income` atau `Expense`.  

**Testing:**  
Mahasiswa harus memastikan bahwa setiap business rule di atas terimplementasi dengan benar dalam unit test. 

**Program.cs untuk Testing Manual:**
```csharp
using System;

namespace PersonalFinanceManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var account = new Account("Personal Savings", new Amount(1000));
                Console.WriteLine(account);

                // Test Income Transaction
                var transaction1 = new Transaction(new Amount(500), new TransactionCategory("Income"), new TransactionDate(DateTime.Now), new TransactionDescription("Freelance Project"));
                account.AddTransaction(transaction1);
                Console.WriteLine(account);

                // Test Expense Transaction
                var transaction2 = new Transaction(new Amount(200), new TransactionCategory("Expense"), new TransactionDate(DateTime.Now), new TransactionDescription("Groceries"));
                account.AddTransaction(transaction2);
                Console.WriteLine(account);

                // Test Insufficient Balance (should throw exception)
                var transaction3 = new Transaction(new Amount(2000), new TransactionCategory("Expense"), new TransactionDate(DateTime.Now), new TransactionDescription("Laptop Purchase"));
                account.AddTransaction(transaction3);
                Console.WriteLine(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
```
---

### Tugas 2: Sistem Reservasi Hotel (Hotel Reservation System)

**Deskripsi Kasus:**  
Sistem ini digunakan untuk mengelola reservasi kamar di hotel. Setiap reservasi memiliki informasi mengenai tamu, periode tinggal, tipe kamar, dan total biaya. Sistem harus memastikan setiap reservasi valid dan memenuhi aturan bisnis yang berlaku.

**Kelas yang Terlibat:**  
- **Entity:** Reservation, Guest, Room  
- **Value Objects:** RoomType, GuestName, GuestContact, ReservationPeriod, ReservationAmount  

**Detail Implementasi:**
1. **RoomType (Value Object)**  
   - Menyimpan tipe kamar yang valid.  
   - Tipe yang diizinkan: `Standard`, `Deluxe`, `Suite`.  
   - Tipe lain selain ini harus menghasilkan error.  
   - Contoh: `new RoomType("Standard")`  

2. **GuestName (Value Object)**  
   - Menyimpan nama tamu.  
   - Nama tidak boleh kosong atau hanya berisi spasi.  
   - Contoh: `new GuestName("John Doe")`  

3. **Reservation (Entity)**  
   - Menyimpan informasi tentang reservasi termasuk tipe kamar, nama tamu, tanggal check-in, dan tanggal check-out.  
   - Tanggal check-in tidak boleh berada di masa lalu.  
   - Tanggal check-out harus setelah tanggal check-in.  
   - Dapat diubah menggunakan metode `ModifyReservation()` sebelum periode reservasi dimulai.  
   - Dapat dibatalkan menggunakan metode `CancelReservation()` sebelum periode reservasi dimulai.  
   - Contoh:  
     ```csharp
     var reservation = new Reservation(new RoomType("Standard"), new GuestName("John Doe"), DateTime.Now.AddDays(1), DateTime.Now.AddDays(5));
     reservation.ModifyReservation(DateTime.Now.AddDays(2), DateTime.Now.AddDays(6));
     reservation.CancelReservation();
     ```  

4. **Room (Entity)**  
   - Menyimpan informasi tentang kamar termasuk tipe kamar dan kapasitas.  
   - Memeriksa ketersediaan kamar sebelum reservasi.  
   - Mengurangi kapasitas ketika kamar dipesan dan mengembalikan kapasitas ketika reservasi dibatalkan.  
   - Contoh:  
     ```csharp
     var room = new Room(new RoomType("Deluxe"), 5);
     if (room.IsAvailable())
     {
         room.Reserve();
     }
     ```  

**Business Rules:**  
1. Periode reservasi (`ReservationPeriod`) tidak boleh berada di masa lalu.  
2. Reservasi tidak bisa dibuat jika kamar (`RoomType`) yang diinginkan sudah penuh.  
3. Biaya reservasi (`ReservationAmount`) harus sesuai dengan tarif kamar dan periode tinggal.  
4. Tipe kamar (`RoomType`) harus valid sesuai dengan data yang ada (`Standard`, `Deluxe`, `Suite`).  
5. Perubahan reservasi hanya bisa dilakukan sebelum periode reservasi dimulai.  
6. Nama tamu (`GuestName`) tidak boleh kosong.  
7. Kontak tamu (`GuestContact`) harus berupa nomor telepon atau email yang valid.  
8. Reservasi dapat dibatalkan sebelum periode reservasi dimulai, dan kapasitas kamar harus dikembalikan.  

**Program.cs untuk Testing Manual:**
```csharp
using System;

namespace HotelReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Membuat reservasi valid
                var reservation = new Reservation(new RoomType("Deluxe"), new GuestName("Jane Doe"), DateTime.Now.AddDays(2), DateTime.Now.AddDays(7));
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
}
```
---

### Tugas 3: Sistem Manajemen Inventaris Toko (Retail Inventory Management System)

**Deskripsi Kasus:**  
Sistem ini digunakan untuk mengelola inventaris produk di toko, termasuk stok barang, harga, dan kategori produk. Sistem harus memastikan setiap produk memiliki detail yang lengkap dan valid, serta memungkinkan pengelolaan stok secara aman. Selain itu, sistem juga harus mendukung fitur diskon, bundling, dan pengelolaan produk kedaluwarsa.

**Kelas yang Terlibat:**  
- **Entity:** Product, ProductBundle  
- **Value Objects:** ProductName, ProductCategory, Price, StockQuantity, ExpiryDate, Discount  

**Detail Implementasi:**
1. **ProductName (Value Object)**  
   - Menyimpan nama produk.  
   - Nama produk tidak boleh kosong atau hanya berisi spasi.  
   - Contoh: `new ProductName("Laptop")`  

2. **ProductCategory (Value Object)**  
   - Menyimpan kategori produk yang valid.  
   - Kategori yang diizinkan: `Electronics`, `Groceries`, `Clothing`, `Furniture`.  
   - Kategori lain selain ini harus menghasilkan error.  
   - Contoh: `new ProductCategory("Electronics")`  

3. **Price (Value Object)**  
   - Menyimpan harga produk.  
   - Harga tidak boleh negatif atau nol.  
   - Menggunakan tipe data `decimal` untuk menghindari masalah presisi.  
   - Contoh: `new Price(1500.00m)`  

4. **StockQuantity (Value Object)**  
   - Menyimpan jumlah stok produk.  
   - Jumlah stok tidak boleh negatif.  
   - Contoh: `new StockQuantity(50)`  

5. **ExpiryDate (Value Object)**  
   - Menyimpan tanggal kedaluwarsa untuk produk yang memiliki umur simpan.  
   - Tanggal kedaluwarsa tidak boleh berada di masa lalu saat produk dibuat.  
   - Contoh: `new ExpiryDate(DateTime.Now.AddDays(30))`  

6. **Discount (Value Object)**  
   - Menyimpan diskon produk dalam persentase (`0-100%`).  
   - Diskon tidak boleh negatif atau melebihi 100%.  
   - Contoh: `new Discount(10)`  

7. **Product (Entity)**  
   - Menyimpan detail lengkap tentang produk termasuk nama, kategori, harga, jumlah stok, diskon, dan tanggal kedaluwarsa (jika ada).  
   - Dapat menambah dan mengurangi stok produk.  
   - Dapat menerapkan diskon dan menghitung harga akhir.  
   - Dapat menandai produk sebagai kedaluwarsa.  
   - Contoh:  
     ```csharp
     var product = new Product(new ProductName("Laptop"), new ProductCategory("Electronics"), new Price(2000.00m), new StockQuantity(50));
     product.AddStock(20);
     product.ReduceStock(10);
     product.SetDiscount(new Discount(10));
     Console.WriteLine(product.GetFinalPrice()); // 1800.00
     ```  

8. **ProductBundle (Entity)**  
   - Menyimpan bundel produk dengan harga khusus.  
   - Mengelola stok semua produk yang termasuk dalam bundel.  
   - Contoh:  
     ```csharp
     var laptop = new Product(new ProductName("Laptop"), new ProductCategory("Electronics"), new Price(2000.00m), new StockQuantity(10));
     var mouse = new Product(new ProductName("Mouse"), new ProductCategory("Electronics"), new Price(50.00m), new StockQuantity(50));
     var bundle = new ProductBundle("Laptop + Mouse Bundle");
     bundle.AddProduct(laptop);
     bundle.AddProduct(mouse);
     bundle.SetBundlePrice(1900.00m);
     Console.WriteLine(bundle);
     ```  

**Program.cs untuk Testing Manual:**
```csharp
using System;

namespace RetailInventorySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Membuat produk valid
                var laptop = new Product(new ProductName("Laptop"), new ProductCategory("Electronics"), new Price(2000.00m), new StockQuantity(50));
                laptop.SetDiscount(new Discount(10));
                Console.WriteLine($"Product: {laptop}, Final Price: {laptop.GetFinalPrice()}");

                // Mengelola stok produk
                laptop.AddStock(20);
                laptop.ReduceStock(30);
                Console.WriteLine($"After Stock Update: {laptop}");

                // Membuat bundel produk
                var mouse = new Product(new ProductName("Mouse"), new ProductCategory("Electronics"), new Price(50.00m), new StockQuantity(100));
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
}
```


