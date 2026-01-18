using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Persistence;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using FluentValidation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CleanTeeth.Tests.Application.Features.Dentaloffices
{
    [TestClass]
    public class CreateDentalOfficeCommandHandlerTests
    {
        private IDentalOfficeRepository repository;
        private IUnitOfWork unitOfWork;
        private CreateDentalOfficeCommandHandler handler;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            unitOfWork  = Substitute.For<IUnitOfWork>();
            handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_ReturnsDentalOfficeId()
        {
            var command = new CreateDentalOfficeCommand { Name = "Dental Office A" };

            var dentalOffice = new DentalOffice("Dental Office A");

            repository.Add(Arg.Any<DentalOffice>()).Returns(dentalOffice);

            var result = await handler.Handle(command);

            await repository.Received(1).Add(Arg.Any<DentalOffice>());
            await unitOfWork.Received(1).CommitAsync();
            Assert.AreEqual(dentalOffice.Id, result);
        }

        [TestMethod]
        public async Task Handle_WhenThereAnError_WeRollback()
        {
            var command = new CreateDentalOfficeCommand { Name = "Dental Office A" };

            repository.Add(Arg.Any<DentalOffice>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await handler.Handle(command);
            });

            await unitOfWork.Received(1).RollbackAsync();
        }
    }
}
