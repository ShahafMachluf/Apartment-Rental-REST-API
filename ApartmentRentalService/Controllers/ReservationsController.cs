using ApartmentRentalService.Data;
using ApartmentRentalService.Dtos;
using ApartmentRentalService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApartmentRentalService.Controllers
{
    [Route("api/Reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IHostRepo _hostRepo;
        private readonly IGuestRepo _guestRepo;
        private readonly IReservationRepo _reservationRepo;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationRepo reservationRepo, IGuestRepo guestRepo, IHostRepo hostRepo,
            IMapper mapper)
        {
            _hostRepo = hostRepo;
            _guestRepo = guestRepo;
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationReadDto>> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = _reservationRepo.GetAllReservations();
            IEnumerable<ReservationReadDto> reservationsReadDtos = _mapper.Map<IEnumerable<ReservationReadDto>>(reservations);
            return Ok(reservationsReadDtos);
        }

        [HttpGet("{id}", Name = "GetReservationById")]
        public ActionResult<ReservationReadDto> GetReservationById(int id)
        {
            Reservation reservation = _reservationRepo.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ReservationReadDto reservationReadDto = _mapper.Map<ReservationReadDto>(reservation);
            return Ok(reservationReadDto);
        }

        [HttpPost]
        public ActionResult<ReservationReadDto> CreateReservation([FromQueryAttribute] ReservationCreateDto reservationCreateDto)
        {
            Reservation reservationModel = _mapper.Map<Reservation>(reservationCreateDto);
            try
            {
                IsValidReservation(reservationModel.ReservationGuestId, reservationModel.ReservationHostId, reservationModel.ArrivingDate, reservationModel.LeavingDate);
            }
            catch(ArgumentException exp)
            {
                return NotFound(exp.Message);
            }

            _reservationRepo.CreateReservation(reservationModel);
            _reservationRepo.SaveChanges();
            ReservationReadDto reservationReadDto = _mapper.Map<ReservationReadDto>(reservationModel);

            return CreatedAtRoute(nameof(GetReservationById), new { id = reservationModel.Id }, reservationReadDto);
        }

        private void IsValidReservation(int guestId, int hostId, DateTime arrivingDate, DateTime leavingDate)
        {
            if(!IsGuestExist(guestId))
            {
                throw new ArgumentException($"Guest id: {guestId} not exist.");
            }
            if(!IsHostExist(hostId))
            {
                throw new ArgumentException($"Host id: {hostId} not exist.");
            }
            if(!IsValidDates(arrivingDate, leavingDate))
            {
                throw new ArgumentException($"Invalid dates. Arriving date should be before leaving date");
            }
            if(!IsApartmentAvailableForReservation(hostId, arrivingDate, leavingDate))
            {
                throw new ArgumentException($"Apartment of host id: {hostId} is not available between {arrivingDate.ToShortDateString()} to {leavingDate.ToShortDateString()}");
            }
            if(!IsGuestAvailable(guestId, arrivingDate, leavingDate))
            {
                throw new ArgumentException($"Guest with id: {guestId} already reserved an appartment between {arrivingDate.ToShortDateString()} to {leavingDate.ToShortDateString()}");
            }
        }

        private bool IsValidDates(DateTime arrivingDate, DateTime leavingDate)
        {
            return arrivingDate < leavingDate;
        }

        private bool IsGuestAvailable(int reservationGuestId, DateTime arrivingDate, DateTime leavingDate)
        {
            var guestReservations = _reservationRepo.GetAllReservationsOfGuest(reservationGuestId);
            var relevantGuesReservations = guestReservations.Where(reserv => reserv.LeavingDate > arrivingDate).ToList();

            foreach (Reservation reservation in relevantGuesReservations)
            {
                if (reservation.LeavingDate < leavingDate || (reservation.ArrivingDate > arrivingDate && reservation.ArrivingDate < leavingDate))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsApartmentAvailableForReservation(int reservationHostId, DateTime arrivingDate, DateTime leavingDate)
        {
            var hostReservations = _reservationRepo.GetAllReservationsOfGuest(reservationHostId);
            var relevantHostReservations = hostReservations.Where(resrv => resrv.LeavingDate > arrivingDate).ToList();

            foreach(Reservation reservation in relevantHostReservations)
            {
                if (reservation.LeavingDate < leavingDate || (reservation.ArrivingDate > arrivingDate && reservation.ArrivingDate < leavingDate))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsHostExist(int reservationHostId)
        {
            return _hostRepo.GetHostById(reservationHostId) != null;
        }

        private bool IsGuestExist(int reservationGuestId)
        {
            return _guestRepo.GetGuestById(reservationGuestId) != null;
        }
    }
}
