using ApartmentRentalService.Data;
using ApartmentRentalService.Dtos;
using ApartmentRentalService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Controllers
{
    [Route("api/Hosts")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly IHostRepo _repo;
        private readonly IMapper _mapper;

        public HostsController(IHostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HostReadDto>> GetAllHosts()
        {
            var hostsModel = _repo.GetAllHosts();
            var hostsReadDto = _mapper.Map<IEnumerable<HostReadDto>>(hostsModel);
            return Ok(hostsReadDto);
        }

        [HttpGet("{id}", Name= "GetHostById")]
        public ActionResult<HostReadDto> GetHostById(int id)
        {
            var hostModel = _repo.GetHostById(id);
            if(hostModel == null)
            {
                return NotFound();
            }
            var hostReadDto = _mapper.Map<HostReadDto>(hostModel);
            return Ok(hostReadDto);
        }

        [HttpPost]
        public ActionResult<HostReadDto> CreateHost([FromBody] HostCreateDto hostCreateDto)
        {
            Host hostModel = _mapper.Map<Host>(hostCreateDto);
            hostModel.Apartment.NumberOfTimesReserved = 0;
            _repo.CreateHost(hostModel);
            _repo.SaveChanges();
            HostReadDto hostReadDto = _mapper.Map<HostReadDto>(hostModel);

            return CreatedAtRoute(nameof(GetHostById), new { id = hostReadDto.Id }, hostReadDto);
        }
    }
}
