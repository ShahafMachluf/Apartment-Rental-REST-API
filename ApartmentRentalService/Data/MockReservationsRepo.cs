using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class MockReservationsRepo : IReservationRepo
    {
        private MockGuestsRepo Guests = new MockGuestsRepo();
        private MockHostsRepo Hosts = new MockHostsRepo();
        private List<Reservation> Reservations;

        public MockReservationsRepo()
        {
            Reservations = new List<Reservation> 
            {
                new Reservation
                {
                    Id = 1,
                    ArrivingDate = new DateTime(2019 ,08 , 01),
                    LeavingDate = new DateTime(2019 , 08 , 03),
                    ReservationGuestId = 1,
                    ReservationHost = Hosts.GetHostById(1),
                    ReservationHostId = 2,
                    ReservationGuest = Guests.GetGuestById(2)
                },
                new Reservation
                {
                    Id = 2,
                    ArrivingDate = new DateTime(2019 , 07 , 01),
                    LeavingDate = new DateTime(2019 , 07 , 03),
                    ReservationHostId = 3,
                    ReservationHost = Hosts.GetHostById(3),
                    ReservationGuestId = 1,
                    ReservationGuest = Guests.GetGuestById(1)
                },
                 new Reservation
                {
                    Id = 3,
                    ArrivingDate = new DateTime(2019 , 07 , 05),
                    LeavingDate = new DateTime(2019 , 07 , 10),
                    ReservationHostId = 2,
                    ReservationHost = Hosts.GetHostById(2),
                    ReservationGuestId = 1,
                    ReservationGuest = Guests.GetGuestById(1)
                }
            };
        }

        public void CreateReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return Reservations;
        }

        public IEnumerable<Reservation> GetAllReservationsOfGuest(int guestId)
        {
            return Reservations.Where(resrv => resrv.ReservationGuestId == guestId).ToList();
        }

        public IEnumerable<Reservation> GetAllReservationsOfHost(int hostId)
        {
            return Reservations.Where(resrv => resrv.ReservationGuestId == hostId).ToList();
        }

        public Reservation GetReservationById(int id)
        {
            if (id > Reservations.Count || id < 1)
            {
                return null;
            }
            return Reservations[id - 1];
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
