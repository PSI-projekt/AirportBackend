using Airport.Domain.DTOs;
using Airport.Domain.Models;
using AutoMapper;

namespace AirportBackend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<AirportForAddDto, AirportEntity>();
            CreateMap<AirplaneForAddDto, Airplane>();
        }
    }
}