using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class SqlReservationsRepo : IReservationRepo
    {
        private readonly ReservationContext _context;

        public SqlReservationsRepo(ReservationContext context)
        {
            _context = context;
        }

        public void CreateReservation(Reservation reservation)
        {
            if(reservation == null)
            {
                throw new ArgumentNullException("Null reservation");
            }
            _context.Add(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.FirstOrDefault(reservation => reservation.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Reservation> GetAllReservationsOfGuest(int guestId)
        {
            return _context.Reservations.Where(resrv => resrv.ReservationGuestId == guestId).ToList();
        }

        public IEnumerable<Reservation> GetAllReservationsOfHost(int hostId)
        {
            return _context.Reservations.Where(resrv => resrv.ReservationHostId == hostId).ToList();
        }
    }
}
