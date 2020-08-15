using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public interface IGuestRepo
    {
        IEnumerable<Guest> GetAllGuests();
        Guest GetGuestById(int id);
        void CreateGuest(Guest guest);
        bool SaveChanges();
    }
}
