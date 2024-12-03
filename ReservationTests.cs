using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace ReservationTests
{
    [TestFixture]
    public class ReservationManagerTests
    {
        [Test]
        public void AddRestaurant_ShouldAddRestaurantSuccessfully()
        {
            var manager = new ReservationManager();
            manager.AddRestaurant("Test Restaurant", 5);
            Assert.AreEqual(1, manager.GetRestaurantsCount());
        }

        [Test]
        public void AddRestaurant_ShouldNotAddInvalidRestaurant()
        {
            var manager = new ReservationManager();
            manager.AddRestaurant("", -1);
            Assert.AreEqual(0, manager.GetRestaurantsCount());
        }

        [Test]
        public void BookTable_ShouldReturnTrueForValidBooking()
        {
            var manager = new ReservationManager();
            manager.AddRestaurant("Test Restaurant", 5);
            var result = manager.BookTable("Test Restaurant", DateTime.Now, 1);
            Assert.IsTrue(result);
        }

        [Test]
        public void BookTable_ShouldReturnFalseForDuplicateBooking()
        {
            var manager = new ReservationManager();
            manager.AddRestaurant("Test Restaurant", 5);
            manager.BookTable("Test Restaurant", DateTime.Now, 1);
            var result = manager.BookTable("Test Restaurant", DateTime.Now, 1);
            Assert.IsFalse(result);
        }
    }
}
