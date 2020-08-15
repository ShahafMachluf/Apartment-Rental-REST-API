using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Models
{
    [Table("Hosts")]
    public class Host
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("Apartment id")]
        public Apartment Apartment { get; set; }
    }
}
