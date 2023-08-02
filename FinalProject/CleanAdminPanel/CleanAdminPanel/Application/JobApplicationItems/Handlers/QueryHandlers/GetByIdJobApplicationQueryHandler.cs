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
    public class GetByIdJobApplicationQueryHandler : IRequestHandler<GetByIdJobApplicationQueryRequest, GetByIdJobApplicationQueryResponse>
    {
        private readonly IApplicationDbContext? _context;

        public GetByIdJobApplicationQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<GetByIdJobApplicationQueryResponse> Handle(GetByIdJobApplicationQueryRequest request, CancellationToken cancellationToken)
        {

            var JobApplication = _context.JobApplications.FirstOrDefault(p => p.Id == request.Id);
            return new GetByIdJobApplicationQueryResponse
            {
                Id = JobApplication.Id,
                Name = JobApplication.Name,
                Email = JobApplication.Email,
                Phone = JobApplication.Phone,
                Cv = JobApplication.Cv,
                Linkedin = JobApplication.Linkedin
            };
        }
    }
}
