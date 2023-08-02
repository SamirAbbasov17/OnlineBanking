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
    public class UpdateHelpCommandHandler : IRequestHandler<UpdateHelpCommandRequest, UpdateHelpCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public UpdateHelpCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateHelpCommandResponse> Handle(UpdateHelpCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Name == null)
            {
                var Help = _context.Helps.FirstOrDefault(p => p.Id == request.Id);
                return new UpdateHelpCommandResponse
                {
                    Id = Help.Id,
                    Name = Help.Name,
                    Description = Help.Description,
                    Content = Help.Content,
                };
            }
            var updateProduct = _context.Helps.FirstOrDefault(p => p.Id == request.Id);

            updateProduct.Name = request.Name;
            updateProduct.Description = request.Description;
            updateProduct.Content = request.Content;
            _context.Helps.Update(updateProduct);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateHelpCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
