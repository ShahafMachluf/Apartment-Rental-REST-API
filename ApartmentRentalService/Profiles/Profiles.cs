using ApartmentRentalService.Dtos;
using ApartmentRentalService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRentalService.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<HostCreateDto, Host>();
            CreateMap<ApartmentCreateDto, Apartment>();
            CreateMap<Host, HostReadDto>();
            CreateMap<Apartment, ApartmentReadDto>();
            CreateMap<Address, AddressReadDto>();
            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<Reservation, ReservationReadDto>();
        }
    }
}
