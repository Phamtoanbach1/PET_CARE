using AutoMapper;
using PetProject.Application.DTOs;
using PetProject.Domain.Entities;

namespace PetProject.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductListDto>();
            CreateMap<Service, ServiceListDto>();
            CreateMap<BookingCreateDto, Appointment>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
