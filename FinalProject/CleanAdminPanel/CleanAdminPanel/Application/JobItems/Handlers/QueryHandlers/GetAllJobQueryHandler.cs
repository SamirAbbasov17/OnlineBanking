using Application.Common.Interfaces;
using Application.JobItems.Queries.Request;
using Application.JobItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Handlers.QueryHandlers
{
    public class GetAllJobQueryHandler : IRequestHandler<GetAllJobQueryRequest, List<GetAllJobQueryResponse>>
    {
        private readonly IApplicationDbContext? _context;

        public GetAllJobQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<List<GetAllJobQueryResponse>> Handle(GetAllJobQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context?.Jobs != null)
            {
                return _context.Jobs.Select(request => new GetAllJobQueryResponse
                {
                    Id = request.Id,
                    JobName = request.JobName,
                    JobDescription = request.JobDescription,
                    JobTitle = request.JobTitle,
                    JobTime = request.JobTime,
                    Experience = request.Experience,
                    Created = request.Created,
                    JobRequirementList = request.JobRequirementList
                }).ToList();
            }
            else
            {
                return new List<GetAllJobQueryResponse>();
            }
        }
    }
}
