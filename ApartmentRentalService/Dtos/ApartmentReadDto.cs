using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApartmentRentalService.Dtos
{
    public class ApartmentReadDto
    {
        [Required]
        public AddressReadDto Address { get; set; }
        [Required]
        [JsonPropertyName("Number of times reserved")]
        public int NumberOfTimesReserved { get; set; }
    }
}
