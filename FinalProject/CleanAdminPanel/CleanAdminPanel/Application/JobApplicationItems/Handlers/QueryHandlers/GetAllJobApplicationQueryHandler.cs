using Application.Common.Interfaces;
using Application.JobApplicationItems.Queries.Request;
using Application.JobApplicationItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Handlers.QueryHandlers
{
    public class GetAllJobApplicationQueryHandler : IRequestHandler<GetAllJobApplicationQueryRequest, List<GetAllJobApplicationQueryResponse>>
    {
        private readonly IApplicationDbContext? _context;

        public GetAllJobApplicationQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<List<GetAllJobApplicationQueryResponse>> Handle(GetAllJobApplicationQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context?.JobApplications != null)
            {
                return _context.JobApplications.Select(request => new GetAllJobApplicationQueryResponse
                {
                    Id = request.Id,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Cv = request.Cv,
                    Linkedin = request.Linkedin
                }).ToList();
            }
            else
            {
                return new List<GetAllJobApplicationQueryResponse>();
            }
        }
    }
}
