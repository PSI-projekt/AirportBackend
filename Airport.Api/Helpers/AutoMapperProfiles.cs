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
            CreateMap<FlightForAddDto, Flight>();
            CreateMap<FlightForEditDto, Flight>();
            CreateMap<Flight, FlightForListDto>();
            CreateMap<AirportEntity, AirportForFlightDto>();
            CreateMap<EmployeeForAddDto, User>();
            CreateMap<PassengerForBookingDto, Passenger>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Passenger, PassengerForListDto>();
            CreateMap<User, UserForDetailsDto>();
        }
    }
}