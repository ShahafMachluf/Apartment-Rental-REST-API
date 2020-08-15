using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class SqlGuestsRepo : IGuestRepo
    {
        private readonly GuestContext _context;

        public SqlGuestsRepo(GuestContext context)
        {
            _context = context;
        }

        public void CreateGuest(Guest guest)
        {
            if(guest == null)
            {
                throw new ArgumentNullException("Null guest");
            }

            _context.Add(guest);
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return _context.Guests.ToList();
        }

        public Guest GetGuestById(int id)
        {
            return _context.Guests.FirstOrDefault(guest => guest.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
