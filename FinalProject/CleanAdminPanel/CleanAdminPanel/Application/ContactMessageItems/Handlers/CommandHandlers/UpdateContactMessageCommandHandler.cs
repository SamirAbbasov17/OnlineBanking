using Application.ContactMessageItems.Commands.Request;
using Application.ContactMessageItems.Commands.Response;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.ContactMessageItems.Handlers.CommandHandlers
{
    public class UpdateContactMessageCommandHandler : IRequestHandler<UpdateContactMessageCommandRequest, UpdateContactMessageCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public UpdateContactMessageCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateContactMessageCommandResponse> Handle(UpdateContactMessageCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Name == null)
            {
                var ContactMessage = _context.ContactMessages.FirstOrDefault(p => p.Id == request.Id);
                return new UpdateContactMessageCommandResponse
                {
                    Id = ContactMessage.Id,
                    Name = ContactMessage.Name,
                    Email = ContactMessage.Email,
                    Phone = ContactMessage.Phone,
                    Company = ContactMessage.Company,
                    MessageTitle = ContactMessage.MessageTitle,
                    MessageBody = ContactMessage.MessageBody
                };
            }
            var updateProduct = _context.ContactMessages.FirstOrDefault(p => p.Id == request.Id);

            updateProduct.Name = request.Name;
            updateProduct.Email = request.Email;
            updateProduct.Phone = request.Phone;
            updateProduct.Company = request.Company;
            updateProduct.MessageTitle = request.MessageTitle;
            updateProduct.MessageBody = request.MessageBody;
            _context.ContactMessages.Update(updateProduct);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateContactMessageCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
