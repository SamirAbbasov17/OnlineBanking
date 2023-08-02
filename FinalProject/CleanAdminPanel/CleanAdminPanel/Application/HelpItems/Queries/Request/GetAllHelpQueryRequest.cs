using Application.ContactMessageItems.Queries.Response;
using Application.HelpItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Queries.Request
{
    public class GetAllHelpQueryRequest : IRequest<List<GetAllHelpQueryResponse>>
    {
    }
}
