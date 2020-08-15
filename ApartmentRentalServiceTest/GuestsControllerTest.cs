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

namespace ApartmentRentalServiceTest
{
    [TestClass]
    public class GuestsControllerTest
    {
        private MockGuestsRepo _repo;
        private GuestsController _controller;

        [TestInitialize]
        public void MockInitialize()
        {
            _repo = new MockGuestsRepo();
            _controller = new GuestsController(_repo);
        }

        [TestMethod]
        public void GetAllGuests_ShouldReturnAllGuests()
        {
            ActionResult<IEnumerable<Guest>> actionResult = _controller.GetAllGuests();
            Assert.IsTrue(actionResult.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetGuestById_ShouldReturnGuest()
        {
            ActionResult<Guest> actionResult = _controller.GetGuestById(1);
            Assert.IsTrue(actionResult.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetGuestById_ShouldReturnNull()
        {
            ActionResult<Guest> actionResult = _controller.GetGuestById(-1);
            Assert.IsTrue(actionResult.Result is NotFoundResult);
        }

        [TestMethod]
        public void CreateGuest_ShouldCreateGuest()
        {
            Guest newGuest = new Guest();
            newGuest.Id = _repo.GetNumberOfGuests()+1;
            ActionResult<Guest> actionResult = _controller.CreateGuest(newGuest);
            Assert.IsTrue(actionResult.Result is CreatedAtRouteResult);
        }
    }
}
