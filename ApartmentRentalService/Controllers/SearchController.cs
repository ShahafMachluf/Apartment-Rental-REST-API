using ApartmentRentalService.Data;
using ApartmentRentalService.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Controllers
{
    [Route("/api/Search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IHostRepo _repo;
        private readonly IMapper _mapper;

        public SearchController(IHostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HostReadDto>> GetMostPopularApartments([FromQueryAttribute] string country, [FromQueryAttribute] string city)
        {
            var mostPopularApartments = _repo.GetMostPopularApartments(country, city);
            if(mostPopularApartments.Count() == 0)
            {
                return NotFound("No apartments found");
            }
            var mostPopularApartmentsReadDto = _mapper.Map<IEnumerable<HostReadDto>>(mostPopularApartments);
            return Ok(mostPopularApartmentsReadDto);
        }
    }
}
