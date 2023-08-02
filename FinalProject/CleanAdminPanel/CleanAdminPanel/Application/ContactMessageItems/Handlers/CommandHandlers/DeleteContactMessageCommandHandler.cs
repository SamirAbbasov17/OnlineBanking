using Application.ContactMessageItems.Commands.Request;
using Application.ContactMessageItems.Commands.Response;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Handlers.CommandHandlers
{
    public class DeleteContactMessageCommandHandler : IRequestHandler<DeleteContactMessageCommandRequest, DeleteContactMessageCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public DeleteContactMessageCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteContactMessageCommandResponse> Handle(DeleteContactMessageCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = _context.ContactMessages.FirstOrDefault(p => p.Id == request.Id);
            _context.ContactMessages.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteContactMessageCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
