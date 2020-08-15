using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApartmentRentalService.Dtos
{
    public class HostReadDto
    {
        [JsonPropertyName("Host id")]
        public int Id { get; set; }
        [Required]
        public ApartmentReadDto Apartment { get; set; }
    }
}
