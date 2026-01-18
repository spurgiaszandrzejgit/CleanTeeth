using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail
{
    public class GetDentalOfficeDetailQueryHandler : IRequestHandler<GetDentalOfficeDetailQuery, GetDentalOfficeDetailDTO>
    {
        private readonly IDentalOfficeRepository repository;

        public GetDentalOfficeDetailQueryHandler(IDentalOfficeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetDentalOfficeDetailDTO> Handle(GetDentalOfficeDetailQuery request)
        {
            var dentalOffice = await repository.GetById(request.Id);

            if (dentalOffice == null)
            {
                throw new NotFoundException();
            }

            return dentalOffice.ToDTO();
        }
    }
}
