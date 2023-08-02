using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.Common.Interfaces;
using Application.ContactMessageItems.Commands.Request;
using Application.ContactMessageItems.Commands.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Handlers.CommandHandlers
{
    public class CreateContactMessageCommandHandler : IRequestHandler<CreateContactMessageCommandRequest, CreateContactMessageCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public CreateContactMessageCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateContactMessageCommandResponse> Handle(CreateContactMessageCommandRequest request, CancellationToken cancellationToken)
        {
            ContactMessage entity = (new()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Company = request.Company,
                MessageTitle = request.MessageTitle,
                MessageBody = request.MessageBody
            });
            _context?.ContactMessages.Add(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            return new CreateContactMessageCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
