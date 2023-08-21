using Application.Common.Interfaces;
using Application.JobItems.Commands.Request;
using Application.JobItems.Commands.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Handlers.CommandHandlers
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommandRequest, CreateJobCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public CreateJobCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateJobCommandResponse> Handle(CreateJobCommandRequest request, CancellationToken cancellationToken)
        {
            Job entity = (new()
            {
                JobName = request.JobName,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                JobTime = request.JobTime,
                Experience = request.Experience
            });
            _context?.Jobs.Add(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            if (request.JobRequirementList != null)
            {
                foreach (var item in request.JobRequirementList)
                {
                    JobRequirement jr = (new()
                    {
                        Name = item.Name,
                        JobId = entity.Id

                    });
                    _context?.JobRequirements.Add(jr);
                }
            }
           
            await _context?.SaveChangesAsync(cancellationToken);
            entity.JobRequirementList = request.JobRequirementList;
            _context?.Jobs.Update(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            return new CreateJobCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
