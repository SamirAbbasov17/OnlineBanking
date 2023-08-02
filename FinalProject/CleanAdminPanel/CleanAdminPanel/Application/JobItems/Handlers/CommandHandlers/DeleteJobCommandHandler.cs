using Application.Common.Interfaces;
using Application.JobItems.Commands.Request;
using Application.JobItems.Commands.Response;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Handlers.CommandHandlers
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommandRequest, DeleteJobCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public DeleteJobCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteJobCommandResponse> Handle(DeleteJobCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = _context.Jobs.FirstOrDefault(p => p.Id == request.Id);
            if (deleteProduct == null)
            {
                // Job with the specified ID not found
                return new DeleteJobCommandResponse
                {
                    IsSuccess = false
                };
            }

            var jobRequirementsToDelete = _context.JobRequirements.Where(p => p.JobId == deleteProduct.Id).ToList();

            foreach (var item in jobRequirementsToDelete)
            {
                _context.JobRequirements.Remove(item);
            }
            _context.Jobs.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);

            var jobRequirementsToDelete2 = _context.JobRequirements.Where(p => p.JobId == null).ToList();
            foreach (var item in jobRequirementsToDelete2)
            {
                _context.JobRequirements.Remove(item);
            }
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteJobCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
