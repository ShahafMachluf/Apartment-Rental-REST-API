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
    public class ReservationsControllerTest
    {
        private MockReservationsRepo _reservationsRepo;
        private MockHostsRepo _hostsRepo;
        private MockGuestsRepo _guestsRepo;
        private ReservationsController _controller;

        [TestInitialize]
        public void MockInitialize()
        {
            _reservationsRepo = new MockReservationsRepo();
            _hostsRepo = new MockHostsRepo();
            _guestsRepo = new MockGuestsRepo();
            Profiles profile = new Profiles();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _controller = new ReservationsController(_reservationsRepo,_guestsRepo,_hostsRepo, new Mapper(configuration));
        }

        [TestMethod]
        public void GetAllReservations_ShouldReturnAllReservations()
        {
            ActionResult<IEnumerable<ReservationReadDto>> reservations = _controller.GetAllReservations();
            Assert.IsTrue( reservations.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetReservationsById_ShouldReturnReservations()
        {
            ActionResult<ReservationReadDto> reservation = _controller.GetReservationById(1);
            Assert.IsTrue(reservation.Result is OkObjectResult);
        }

        [TestMethod]
        public void GetReservationsById_ShouldReturnNull()
        {
            ActionResult<ReservationReadDto> reservation = _controller.GetReservationById(-1);
            Assert.IsTrue(reservation.Result is NotFoundResult);
        }

        [TestMethod]
        public void CreateReservations_ShouldCreateReservations()
        {
            ReservationCreateDto newReservations = new ReservationCreateDto();
            newReservations.ArrivingDate = new DateTime(2018 ,08 , 01);
            newReservations.LeavingDate = new DateTime(2018 , 08 , 09);
            newReservations.ReservationGuestId = 1;
            newReservations.ReservationHostId = 2;
            ActionResult<ReservationReadDto> actionResult = _controller.CreateReservation(newReservations);
            Assert.IsTrue(actionResult.Result is CreatedAtRouteResult);
        }

        [TestMethod]
        public void CreateReservations_ShouldFail()
        {
            ReservationCreateDto newReservations = new ReservationCreateDto();
            newReservations.ArrivingDate = new DateTime(2019,08,02);
            newReservations.LeavingDate = new DateTime(2019,08,04);
            newReservations.ReservationGuestId = 1;
            newReservations.ReservationHostId = 2;
            ActionResult<ReservationReadDto> actionResult = _controller.CreateReservation(newReservations);
            Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        }
    }
}
