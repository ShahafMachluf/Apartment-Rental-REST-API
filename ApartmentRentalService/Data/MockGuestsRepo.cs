using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class MockGuestsRepo : IGuestRepo
    {
        private List<Guest> Guests = new List<Guest>
            {
                new Guest { Id = 1, Name = "Shahaf", Birthday = new DateTime(1995-01-07)},
                new Guest { Id = 2, Name = "Maayan", Birthday = new DateTime(1995-03-16)},
                new Guest { Id = 3, Name = "Mali", Birthday = new DateTime(1960-03-01) }
            };

        public int GetNumberOfGuests()
        {
            return Guests.Count;
        }

        public void CreateGuest(Guest guest)
        {
            Guests.Add(guest);
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return Guests;
        }

        public Guest GetGuestById(int id)
        {
            if(id > Guests.Count || id<1)
            {
                return null;
            }

            return Guests[id - 1];
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
