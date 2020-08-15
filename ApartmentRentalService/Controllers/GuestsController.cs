using ApartmentRentalService.Data;
using ApartmentRentalService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Controllers
{
    [Route("api/Guests")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestRepo _repo;

        public GuestsController(IGuestRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Guest>> GetAllGuests()
        {
            return Ok(_repo.GetAllGuests());
        }

        [HttpGet("{id}", Name = "GetGuestById")]
        public ActionResult<Guest> GetGuestById(int id)
        {
            var guestModel = _repo.GetGuestById(id);
            if(guestModel == null)
            {
                return NotFound();
            }
            return Ok(guestModel);
        }

        [HttpPost]
        public ActionResult<Guest> CreateGuest([FromQueryAttribute] Guest guest)
        {
            _repo.CreateGuest(guest);
            _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetGuestById), new { id = guest.Id }, guest);
        }
    }
}
