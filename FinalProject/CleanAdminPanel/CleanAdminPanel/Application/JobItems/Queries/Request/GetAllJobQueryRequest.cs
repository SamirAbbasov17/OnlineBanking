using Application.JobApplicationItems.Queries.Response;
using Application.JobItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Queries.Request
{
    public class GetAllJobQueryRequest : IRequest<List<GetAllJobQueryResponse>>
    {
    }
}
