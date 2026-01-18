using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail
{
    public class GetDentalOfficeDetailQuery: IRequest<GetDentalOfficeDetailDTO>
    {
        public required Guid Id { get; set; }
    }
}
