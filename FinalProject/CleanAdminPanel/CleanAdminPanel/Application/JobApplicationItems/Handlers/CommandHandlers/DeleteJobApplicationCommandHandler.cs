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
    public class DeleteJobApplicationCommandHandler : IRequestHandler<DeleteJobApplicationCommandRequest, DeleteJobApplicationCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public DeleteJobApplicationCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteJobApplicationCommandResponse> Handle(DeleteJobApplicationCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = _context.JobApplications.FirstOrDefault(p => p.Id == request.Id);
            _context.JobApplications.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteJobApplicationCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
