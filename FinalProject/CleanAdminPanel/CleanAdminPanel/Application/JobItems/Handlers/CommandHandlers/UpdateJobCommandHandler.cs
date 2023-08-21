using Application.Common.Interfaces;
using Application.JobItems.Commands.Request;
using Application.JobItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Handlers.CommandHandlers
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommandRequest, UpdateJobCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public UpdateJobCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateJobCommandResponse> Handle(UpdateJobCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.JobName == null)
            {
                var Job = _context.Jobs.FirstOrDefault(p => p.Id == request.Id);
                var requirement = _context.JobRequirements.Where(p => p.JobId == request.Id).ToList();
                return new UpdateJobCommandResponse
                {
                    Id = Job.Id,
                    JobName = Job.JobName,
                    JobDescription = Job.JobDescription,
                    JobTitle = Job.JobTitle,
                    JobTime = Job.JobTime,
                    Experience = Job.Experience,
                    JobRequirementList = requirement
                };
            }
            var updateProduct = _context.Jobs.FirstOrDefault(p => p.Id == request.Id);

            updateProduct.JobName = request.JobName;
            updateProduct.JobDescription = request.JobDescription;
            updateProduct.JobTitle = request.JobTitle;
            updateProduct.JobTime = request.JobTime;
            updateProduct.Experience = request.Experience;
            var req = _context.JobRequirements.Where(p => p.JobId == request.Id);
            foreach (var item in req)
            {
                _context.JobRequirements.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            updateProduct.JobRequirementList = request.JobRequirementList;

            foreach (var item in request.JobRequirementList)
            {
                _context.JobRequirements.Update(item);
            }
            _context.Jobs.Update(updateProduct);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateJobCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
