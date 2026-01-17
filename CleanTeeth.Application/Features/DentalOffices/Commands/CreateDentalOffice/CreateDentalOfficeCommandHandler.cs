using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Persistence;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice
{
    public class CreateDentalOfficeCommandHandler: IRequestHandler<CreateDentalOfficeCommand, Guid>
    {
        private readonly IDentalOfficeRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<CreateDentalOfficeCommand> validator;

        public CreateDentalOfficeCommandHandler(IDentalOfficeRepository repository, IUnitOfWork unitOfWork,
            IValidator<CreateDentalOfficeCommand> validator)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public async Task<Guid> Handle(CreateDentalOfficeCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid) 
            {
                throw new CustomValidationException(validationResult);
            }

            var dentalOffice = new DentalOffice(command.Name);
            try
            {
                var result = await repository.Add(dentalOffice);
                await unitOfWork.CommitAsync();
                return result.Id;
            }
            catch (Exception)
            {

                await unitOfWork.RollbackAsync();
                throw;
            }

        }
    }
}
