using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Models
{
    [Table("Apartments")]
    public class Apartment
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public Address Address { get; set; }
        
        [Required]
        [Column("times reserved")]
        public int NumberOfTimesReserved { get; set; }
    }
}
