using Application.Common.Interfaces;
using Application.JobApplicationItems.Commands.Request;
using Application.JobApplicationItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Handlers.CommandHandlers
{
    public class UpdateJobApplicationCommandHandler : IRequestHandler<UpdateJobApplicationCommandRequest, UpdateJobApplicationCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public UpdateJobApplicationCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateJobApplicationCommandResponse> Handle(UpdateJobApplicationCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Name == null)
            {
                var JobApplication = _context.JobApplications.FirstOrDefault(p => p.Id == request.Id);
                return new UpdateJobApplicationCommandResponse
                {
                    Id = JobApplication.Id,
                    Name = JobApplication.Name,
                    Email = JobApplication.Email,
                    Phone = JobApplication.Phone,
                    Cv = JobApplication.Cv,
                    Linkedin = JobApplication.Linkedin
                };
            }
            var updateProduct = _context.JobApplications.FirstOrDefault(p => p.Id == request.Id);

            updateProduct.Name = request.Name;
            updateProduct.Email = request.Email;
            updateProduct.Phone = request.Phone;
            updateProduct.Cv = request.Cv;
            updateProduct.Linkedin = request.Linkedin;
            _context.JobApplications.Update(updateProduct);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateJobApplicationCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
