using ApartmentRentalService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class GuestContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }

        public GuestContext(DbContextOptions<GuestContext> options) : base(options)
        {
        }
    }
}
