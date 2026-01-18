using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using CleanTeeth.Application.Exceptions;
using NSubstitute.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.Tests.Application.Features.Dentaloffices
{
    [TestClass]
    public class GetDentalOfficeDetailQueryHandlerTests
    {
        private IDentalOfficeRepository repository;
        private GetDentalOfficeDetailQueryHandler handler;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            handler = new GetDentalOfficeDetailQueryHandler(repository);
        }

        [TestMethod]
        public async Task Handle_DentalOfficeExist_ReturnsId()
        {
            var dentalOffice = new DentalOffice("Dental Office A");
            var id = dentalOffice.Id;
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetById(id).Returns(dentalOffice);

            var result = await handler.Handle(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("Dental Office A", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_DentalOfficeNotExist_Throws()
        {
            var id = Guid.NewGuid();
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetById(id).ReturnsNull();

            await handler.Handle(query);
        }
    }
}
