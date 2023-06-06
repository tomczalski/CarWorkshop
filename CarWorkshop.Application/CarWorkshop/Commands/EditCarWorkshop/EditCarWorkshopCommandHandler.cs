using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    internal class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IUserContext userContext)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var objectToEdit = await _carWorkshopRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && objectToEdit.CreatedById == user.Id;

            if (!isEditable) 
            {
                return Unit.Value;
            }

            objectToEdit.Description = request.Description;
            objectToEdit.About = request.About;
            objectToEdit.ContactDetalis.PhoneNumber = request.PhoneNumber;
            objectToEdit.ContactDetalis.City = request.City;
            objectToEdit.ContactDetalis.Street = request.Street;
            objectToEdit.ContactDetalis.PostalCode = request.PostalCode;
 
                
            await _carWorkshopRepository.Edit();
            return Unit.Value;

        }
    }
}
