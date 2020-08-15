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
using ApartmentRentalService.Profiles;
using AutoMapper;
using ApartmentRentalService.Dtos;
using System;

namespace ApartmentRentalServiceTest
{
    [TestClass]
    public class EndToEndTest
    {
        private MockReservationsRepo _reservationsRepo;
        private MockHostsRepo _hostsRepo;
        private MockGuestsRepo _guestsRepo;
        private ReservationsController _reservationsController;
        private HostsController _hostsController;
        private GuestsController _guestsController;

        [TestInitialize]
        public void MockInitialize()
        {
            _reservationsRepo = new MockReservationsRepo();
            _hostsRepo = new MockHostsRepo();
            _guestsRepo = new MockGuestsRepo();
            Profiles profile = new Profiles();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            _guestsController = new GuestsController(_guestsRepo);
            _hostsController = new HostsController(_hostsRepo, mapper);
            _reservationsController = new ReservationsController(_reservationsRepo, _guestsRepo, _hostsRepo, mapper);
        }

        [TestMethod]
        public void NewGuestHostReservation_Success()
        {
            Guest newGuest = new Guest();
            newGuest.Id = _guestsRepo.GetNumberOfGuests() + 1;
            ActionResult<Guest> guestActionResult = _guestsController.CreateGuest(newGuest);
            Assert.IsTrue(guestActionResult.Result is CreatedAtRouteResult);

            HostCreateDto newHost = new HostCreateDto();
            newHost.Apartment = new ApartmentCreateDto();
            newHost.Apartment.Address = new Address();
            newHost.Apartment.Address.Country = "Israel";
            newHost.Apartment.Address.City = "Netanya";
            newHost.Apartment.Address.Street = "Beeri";
            ActionResult<HostReadDto> hostActionResult = _hostsController.CreateHost(newHost);
            Assert.IsTrue(hostActionResult.Result is CreatedAtRouteResult);
            CreatedAtRouteResult hostReadDto = hostActionResult.Result as CreatedAtRouteResult;

            ReservationCreateDto newReservations = new ReservationCreateDto();
            newReservations.ArrivingDate = new DateTime(2018, 08, 01);
            newReservations.LeavingDate = new DateTime(2018, 08, 09);
            newReservations.ReservationGuestId = newGuest.Id;
            newReservations.ReservationHostId = ((HostReadDto)hostReadDto.Value).Id;
            ActionResult<ReservationReadDto> reservationSctionResult = _reservationsController.CreateReservation(newReservations);
            Assert.IsTrue(reservationSctionResult.Result is CreatedAtRouteResult);
        }

        [TestMethod]
        public void NewGuestHostReservation_Fail()
        {
            Guest newGuest = new Guest();
            newGuest.Id = _guestsRepo.GetNumberOfGuests() + 1;
            ActionResult<Guest> guestActionResult = _guestsController.CreateGuest(newGuest);
            Assert.IsTrue(guestActionResult.Result is CreatedAtRouteResult);

            HostCreateDto newHost = new HostCreateDto();
            newHost.Apartment = new ApartmentCreateDto();
            newHost.Apartment.Address = new Address();
            newHost.Apartment.Address.Country = "Israel";
            newHost.Apartment.Address.City = "Netanya";
            newHost.Apartment.Address.Street = "Beeri";
            ActionResult<HostReadDto> hostActionResult = _hostsController.CreateHost(newHost);
            Assert.IsTrue(hostActionResult.Result is CreatedAtRouteResult);
            CreatedAtRouteResult hostReadDto = hostActionResult.Result as CreatedAtRouteResult;

            ReservationCreateDto newReservations = new ReservationCreateDto();
            newReservations.ArrivingDate = new DateTime(2018, 08, 01);
            newReservations.LeavingDate = new DateTime(2018, 07, 09);
            newReservations.ReservationGuestId = newGuest.Id;
            newReservations.ReservationHostId = ((HostReadDto)hostReadDto.Value).Id;
            ActionResult<ReservationReadDto> reservationSctionResult = _reservationsController.CreateReservation(newReservations);
            Assert.IsTrue(reservationSctionResult.Result is NotFoundObjectResult);
        }
    }
}