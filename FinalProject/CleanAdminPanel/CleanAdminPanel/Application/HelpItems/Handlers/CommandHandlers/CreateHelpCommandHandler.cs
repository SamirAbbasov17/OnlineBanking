using Application.Common.Interfaces;
using Application.HelpItems.Commands.Request;
using Application.HelpItems.Commands.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Handlers.CommandHandlers
{
    public class CreateHelpCommandHandler : IRequestHandler<CreateHelpCommandRequest, CreateHelpCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public CreateHelpCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateHelpCommandResponse> Handle(CreateHelpCommandRequest request, CancellationToken cancellationToken)
        {
            Help entity = (new()
            {
                Name = request.Name,
                Description = request.Description,
                Content = request.Content
            });
            _context?.Helps.Add(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            return new CreateHelpCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
