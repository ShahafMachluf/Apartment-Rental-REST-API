using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using ApartmentRentalService.Data;
using ApartmentRentalService.Controllers;
using System;
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
    public class SearchControllerTest
    {
        private SearchController _controller;
        private MockHostsRepo _repo;

        [TestInitialize]
        public void MockInitialize()
        {
            _repo = new MockHostsRepo();
            Profiles profile = new Profiles();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _controller = new SearchController(_repo, new Mapper(configuration));
        }

        [TestMethod]
        public void GetMostPopularApartments_ShouldReturnHosts()
        {
            HostReadDto previousHost = null;
            ActionResult<IEnumerable<HostReadDto>> actionResult = _controller.GetMostPopularApartments("Israel", "Tel Aviv");
            OkObjectResult mostPopularHosts = actionResult.Result as OkObjectResult;
            Assert.IsTrue(((IEnumerable<HostReadDto>)mostPopularHosts.Value).Count() <= 10);
            foreach(HostReadDto host in (IEnumerable<HostReadDto>)mostPopularHosts.Value)
            {
                Assert.AreEqual("Israel", host.Apartment.Address.Country);
                Assert.AreEqual("Tel Aviv", host.Apartment.Address.City);
                if (previousHost == null)
                {
                    previousHost = host;
                }
                else
                {
                    Assert.IsTrue(host.Apartment.NumberOfTimesReserved >= previousHost.Apartment.NumberOfTimesReserved);
                    previousHost = host;
                }
            }
        }

        [TestMethod]
        public void GetMostPopularApartments_ShouldFail()
        {
            ActionResult<IEnumerable<HostReadDto>> actionResult = _controller.GetMostPopularApartments("Italy", "Rome");
            Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        }
    }
}
