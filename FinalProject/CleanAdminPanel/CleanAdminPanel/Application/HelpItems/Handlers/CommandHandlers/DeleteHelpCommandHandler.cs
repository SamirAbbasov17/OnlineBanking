using Application.Common.Interfaces;
using Application.HelpItems.Commands.Request;
using Application.HelpItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Handlers.CommandHandlers
{
    public class DeleteHelpCommandHandler : IRequestHandler<DeleteHelpCommandRequest, DeleteHelpCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public DeleteHelpCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteHelpCommandResponse> Handle(DeleteHelpCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = _context.Helps.FirstOrDefault(p => p.Id == request.Id);
            _context.Helps.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteHelpCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
