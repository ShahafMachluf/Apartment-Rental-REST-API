using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public interface IReservationRepo
    {
        IEnumerable<Reservation> GetAllReservations();

        Reservation GetReservationById(int id);

        void CreateReservation(Reservation reservation);

        bool SaveChanges();

        IEnumerable<Reservation> GetAllReservationsOfGuest(int guestId);

        IEnumerable<Reservation> GetAllReservationsOfHost(int hostId);
    }
}
