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
            CreateMap<UserForEditDto, User>();
            CreateMap<AirportForAddDto, AirportEntity>();
            CreateMap<AirplaneForAddDto, Airplane>();
            CreateMap<AirplaneForEditDto, Airplane>();
            CreateMap<FlightForAddDto, Flight>();
            CreateMap<FlightForEditDto, Flight>();
            CreateMap<Flight, FlightForListDto>();
            CreateMap<AirportEntity, AirportForFlightDto>();
            CreateMap<EmployeeForAddDto, User>();
            CreateMap<PassengerForBookingDto, Passenger>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Passenger, PassengerForListDto>();
            CreateMap<User, UserForDetailsDto>();
            CreateMap<AirportEntity, AirportForListDto>();
            CreateMap<AirportForEditDto, AirportEntity>();
            CreateMap<Airplane, AirplaneForListDto>();
            CreateMap<PassengerForEditDto, Passenger>();
            CreateMap<Passenger, PassengerForEditDto>();
            CreateMap<Flight, FlightDetailsDto>();
            CreateMap<Booking, BookingForListDto>();
            CreateMap<Airplane, AirplaneForBookingDto>();
        }
    }
}