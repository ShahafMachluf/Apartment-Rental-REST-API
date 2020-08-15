using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using ApartmentRentalService.Data;
using ApartmentRentalService.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ApartmentRentalService.Models;
using AutoMapper;
using ApartmentRentalService.Profiles;
using ApartmentRentalService.Dtos;

namespace ApartmentRentalServiceTest
{
    [TestClass]
    public class HostsControllerTest
    {
        private MockHostsRepo _repo;
        private HostsController _controller;

        [TestInitialize]
        public void MockInitialize()
        {
            _repo = new MockHostsRepo();
            Profiles profile = new Profiles();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _controller = new HostsController(_repo, new Mapper(configuration));
        }

        [TestMethod]
        public void GetAllHosts_ShouldReturnAllHosts()
        {
            ActionResult<IEnumerable<HostReadDto>> hosts = _controller.GetAllHosts();
            Assert.IsTrue(hosts.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetHostById_ShouldReturnHost()
        {
            ActionResult<HostReadDto> host = _controller.GetHostById(1);
            Assert.IsTrue(host.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetHostById_ShouldReturnNull()
        {
            ActionResult<HostReadDto> host = _controller.GetHostById(-1);
            Assert.IsTrue(host.Result is NotFoundResult);
        }

        [TestMethod]
        public void CreateHost_ShouldCreateHost()
        {
            HostCreateDto newHost = new HostCreateDto();
            newHost.Apartment = new ApartmentCreateDto();
            newHost.Apartment.Address = new Address();
            newHost.Apartment.Address.Country = "Israel"; 
            newHost.Apartment.Address.City = "Netanya"; 
            newHost.Apartment.Address.Street = "Beeri";
            ActionResult<HostReadDto> actionResult = _controller.CreateHost(newHost);
            Assert.IsTrue(actionResult.Result is CreatedAtRouteResult);
        }
    }
}
