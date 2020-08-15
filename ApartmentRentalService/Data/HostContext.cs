using ApartmentRentalService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class HostContext :DbContext
    {
        public DbSet<Host> Hosts{ get; set; }

        public HostContext(DbContextOptions<HostContext> options) : base(options)
        {

        }
    }
}
