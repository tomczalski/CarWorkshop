using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshopService;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();


            CreateMap<CarWorkshopCommand, Domain.Entities.CarWorkshop>()
                .ForMember(e => e.ContactDetalis, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopCommand>()
                .ForMember(dto=>dto.IsEditable, opt => opt.MapFrom(src=> user !=null && src.CreatedById == user.Id))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetalis.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetalis.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetalis.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetalis.PhoneNumber));

            CreateMap<CarWorkshopCommand, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>()
                .ReverseMap();

        }
    }
}
