using SailClubLibrary.Models;
using SailClubLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// Interface for the BookingRepository class
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Adds a new booking to the system.
        /// </summary>
        /// <param name="booking">The booking to add. Cannot be null.</param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidDateException"></exception>
        /// <exception cref="OverlappingDateException"></exception>
        void AddBooking(Booking booking);
        void RemoveBooking(Booking b);
        List<Booking> GetAllBookings();
        void UpdateBooking(int id, Booking newBooking);
        void PrintAll();
        int GetBookingCountForMember(Member member);
        Dictionary<string, int> GetAllBookingsForMembers();
        List<Booking> GetBookingsForBoat(string sailNumber);
        List<Booking> GetOverlappingBookings(string sailNumber, DateTime startDate, DateTime endDate);

    }
}
