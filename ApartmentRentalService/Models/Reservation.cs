using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("Arriving date")]
        public DateTime ArrivingDate { get; set; }

        [Required]
        [Column("Leaving date")]
        public DateTime LeavingDate { get; set; }

        [Required]
        [Column("Host id")]
        [ForeignKey("Host")]
        public int ReservationHostId { get; set; }
        public Host ReservationHost { get; set; }

        [Required]
        [Column("Guest id")]
        [ForeignKey("Guest")]
        public int ReservationGuestId { get; set; }
        public Guest ReservationGuest { get; set; }

    }
}
