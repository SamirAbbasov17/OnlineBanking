using Application.BlogItems.Queries.Response;
using Application.ContactMessageItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Queries.Request
{
    public class GetAllContactMessageQueryRequest : IRequest<List<GetAllContactMessageQueryResponse>>
    {
    }
}
