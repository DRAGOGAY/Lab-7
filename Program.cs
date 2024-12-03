using System;
using System.Collections.Generic;

// Main Application Class
public class TableReservationApplication
{
    static void Main(string[] args)
    {
        var reservationManager = new ReservationManager();
        reservationManager.AddRestaurant("Restaurant A", 10);
        reservationManager.AddRestaurant("Restaurant B", 5);

        Console.WriteLine(reservationManager.BookTable("Restaurant A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(reservationManager.BookTable("Restaurant A", new DateTime(2023, 12, 25), 3)); // False
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
        if (string.IsNullOrWhiteSpace(name) || tableCount <= 0)
        {
            Console.WriteLine("Invalid restaurant details provided.");
            return;
        }

        var restaurant = new Restaurant(name, tableCount);
        _restaurants.Add(restaurant);
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        var restaurant = _restaurants.Find(r => r.Name == restaurantName);
        if (restaurant == null)
        {
            Console.WriteLine($"Restaurant '{restaurantName}' not found.");
            return false;
        }

        return restaurant.BookTable(date, tableNumber);
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
        return true;
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
