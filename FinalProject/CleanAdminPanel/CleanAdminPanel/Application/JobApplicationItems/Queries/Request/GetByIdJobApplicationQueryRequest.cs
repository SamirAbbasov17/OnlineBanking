using Application.HelpItems.Queries.Response;
using Application.JobApplicationItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Queries.Request
{
    public class GetByIdJobApplicationQueryRequest : IRequest<GetByIdJobApplicationQueryResponse>
    {
        public int Id { get; set; }
    }
}
