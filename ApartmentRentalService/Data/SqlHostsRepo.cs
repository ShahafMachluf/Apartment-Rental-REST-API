using ApartmentRentalService.Dtos;
using ApartmentRentalService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class SqlHostsRepo : IHostRepo
    {
        private readonly HostContext _context;

        public SqlHostsRepo(HostContext context)
        {
            _context = context;
        }

        public void CreateHost(Host host)
        {
            if(host == null)
            {
                throw new ArgumentNullException("Null host");
            }

            _context.Add(host);
        }

        public IEnumerable<Host> GetAllHosts()
        {
            return _context.Hosts.Include(host => host.Apartment.Address).ToList();
        }

        public Host GetHostById(int id)
        {
            return _context.Hosts.Include(host => host.Apartment.Address).FirstOrDefault(host => host.Id == id);
        }

        public IEnumerable<Host> GetMostPopularApartments(string country, string city)
        {
            IEnumerable<Host> mostPopularApartments;
            if (city == null)
            {
                mostPopularApartments = _context.Hosts.Include(host => host.Apartment.Address).
                    Where(aprt => aprt.Apartment.Address.Country == country).
                    OrderByDescending(aprt => aprt.Apartment.NumberOfTimesReserved).
                    Take(10).
                    ToList();
            }
            else
            {
                mostPopularApartments = _context.Hosts.Include(host => host.Apartment.Address).
                    Where(aprt => aprt.Apartment.Address.Country == country).
                    Where(aprt => aprt.Apartment.Address.City == city).
                    OrderByDescending(aprt => aprt.Apartment.NumberOfTimesReserved).
                    Take(10).
                    ToList();
            }

            return mostPopularApartments;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
