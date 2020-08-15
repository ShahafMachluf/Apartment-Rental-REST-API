using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Dtos
{
    public class ReservationCreateDto
    {
        public DateTime ArrivingDate { get; set; }

        public DateTime LeavingDate { get; set; }

        public int ReservationHostId { get; set; }

        public int ReservationGuestId { get; set; }
    }
}
