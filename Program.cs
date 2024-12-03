using System;
using System.Collections.Generic;

// Main Application Class
public class TableReservationApplication
{
    static void Main(string[] args)
    {
        try
        {
            var reservationManager = new ReservationManager();
            reservationManager.AddRestaurant("Restaurant A", 10);
            reservationManager.AddRestaurant("Restaurant B", 5);

            // Попытка забронировать столы
            Console.WriteLine(reservationManager.BookTable("Restaurant A", new DateTime(2023, 12, 25), 3)); // True
            Console.WriteLine(reservationManager.BookTable("Restaurant A", new DateTime(2023, 12, 25), 3)); // False
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Reservation Manager Class
public class ReservationManager
{
    private readonly List<Restaurant> _restaurants;

    public ReservationManager()
    {
        _restaurants = new List<Restaurant>();
    }

    public void AddRestaurant(string name, int tableCount)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name) || tableCount <= 0)
                throw new ArgumentException("Invalid restaurant details.");

            var restaurant = new Restaurant(name, tableCount);
            _restaurants.Add(restaurant);
            Console.WriteLine($"Restaurant '{name}' added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding restaurant: {ex.Message}");
        }
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        try
        {
            var restaurant = _restaurants.Find(r => r.Name == restaurantName);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant '{restaurantName}' not found.");
                return false;
            }

            return restaurant.BookTable(date, tableNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error booking table: {ex.Message}");
            return false;
        }
    }
}

// Restaurant Class
public class Restaurant
{
    public string Name { get; }
    private readonly List<Reservation> _reservations;

    public Restaurant(string name, int tableCount)
    {
        Name = name;
        _reservations = new List<Reservation>(tableCount);
    }

    public bool BookTable(DateTime date, int tableNumber)
    {
        try
        {
            if (tableNumber <= 0)
            {
                Console.WriteLine("Invalid table number.");
                return false;
            }

            var reservation = _reservations.Find(r => r.Date == date && r.TableNumber == tableNumber);
            if (reservation != null)
            {
                Console.WriteLine($"Table {tableNumber} is already reserved for {date.ToShortDateString()}.");
                return false;
            }

            _reservations.Add(new Reservation(date, tableNumber));
            Console.WriteLine($"Table {tableNumber} successfully booked for {date.ToShortDateString()}.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error booking table: {ex.Message}");
            return false;
        }
    }
}

// Reservation Class
public class Reservation
{
    public DateTime Date { get; }
    public int TableNumber { get; }

    public Reservation(DateTime date, int tableNumber)
    {
        Date = date;
        TableNumber = tableNumber;
    }
}
