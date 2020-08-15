using ApartmentRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Data
{
    public class MockHostsRepo : IHostRepo
    {
        private List<Host> Hosts = new List<Host>
        {
            new Host{Id = 1, Apartment=
                new Apartment{Id = 1, NumberOfTimesReserved = 0, Address = 
                new Address{Id = 1, Country ="Israel", City = "Tel Aviv", Street ="Hertzel" } }},
            new Host{Id = 2, Apartment=
                new Apartment{Id = 2, NumberOfTimesReserved = 0, Address =
                new Address{Id = 2, Country ="USA", City = "New York", Street ="Wall Streert" } }},
            new Host{Id = 3, Apartment=
                new Apartment{Id = 3, NumberOfTimesReserved = 0, Address =
                new Address{Id = 3, Country ="Israel", City = "Tel Aviv", Street ="Tel Giborim" } }}
        };

        public int GetNumberOfHosts()
        {
            return Hosts.Count;
        }
        public void CreateHost(Host host)
        {
            host.Id = Hosts.Count;
            Hosts.Add(host);
        }

        public IEnumerable<Host> GetAllHosts()
        {
            return Hosts;
        }

        public Host GetHostById(int id)
        {
            if (id > Hosts.Count || id < 1)
            {
                return null;
            }
            return Hosts[id - 1];
        }

        public IEnumerable<Host> GetMostPopularApartments(string country, string city)
        {
            if(city != null)
            {
                return Hosts.Where(host => host.Apartment.Address.Country == country).
                    Where(host => host.Apartment.Address.City == city).
                    OrderByDescending(host => host.Apartment.NumberOfTimesReserved).
                    Take(10).
                    ToList();
            }
            else 
            {
                return Hosts.Where(host => host.Apartment.Address.Country == country).
                    OrderByDescending(host => host.Apartment.NumberOfTimesReserved).
                    Take(10).
                    ToList();
            }
            
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
