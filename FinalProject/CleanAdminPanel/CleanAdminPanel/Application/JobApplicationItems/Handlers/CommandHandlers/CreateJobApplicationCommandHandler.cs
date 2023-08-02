using Application.Common.Interfaces;
using Application.JobApplicationItems.Commands.Request;
using Application.JobApplicationItems.Commands.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Handlers.CommandHandlers
{
    public class CreateJobApplicationCommandHandler : IRequestHandler<CreateJobApplicationCommandRequest, CreateJobApplicationCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public CreateJobApplicationCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateJobApplicationCommandResponse> Handle(CreateJobApplicationCommandRequest request, CancellationToken cancellationToken)
        {
            JobApplication entity = (new()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Cv = request.Cv,
                Linkedin = request.Linkedin
            });
            _context?.JobApplications.Add(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            return new CreateJobApplicationCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
