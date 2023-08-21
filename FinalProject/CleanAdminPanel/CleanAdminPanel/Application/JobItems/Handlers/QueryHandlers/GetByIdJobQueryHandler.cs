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
    public class GetByIdJobQueryHandler : IRequestHandler<GetByIdJobQueryRequest, GetByIdJobQueryResponse>
    {
        private readonly IApplicationDbContext? _context;

        public GetByIdJobQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<GetByIdJobQueryResponse> Handle(GetByIdJobQueryRequest request, CancellationToken cancellationToken)
        {

            var Job = _context.Jobs.FirstOrDefault(p => p.Id == request.Id);
            var JobRequirement = _context.JobRequirements.Where(p => p.JobId == request.Id).ToList();
            return new GetByIdJobQueryResponse
            {
                Id = Job.Id,
                JobName = Job.JobName,
                JobDescription = Job.JobDescription,
                JobTitle = Job.JobTitle,
                JobTime = Job.JobTime,
                Experience = Job.Experience,
                Created = Job.Created,
                JobRequirementList = JobRequirement
            };
        }
    }
}
