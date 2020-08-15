using ApartmentRentalService.Dtos;
using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public interface IHostRepo
    {
        IEnumerable<Host> GetAllHosts();

        Host GetHostById(int id);

        void CreateHost(Host host);

        bool SaveChanges();

        IEnumerable<Host> GetMostPopularApartments(string country, string city);
    }
}
