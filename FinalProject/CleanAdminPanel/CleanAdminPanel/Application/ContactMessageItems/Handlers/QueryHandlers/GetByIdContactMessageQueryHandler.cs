using Application.ContactMessageItems.Queries.Request;
using Application.ContactMessageItems.Queries.Response;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Handlers.QueryHandlers
{
    public class GetByIdContactMessageQueryHandler : IRequestHandler<GetByIdContactMessageQueryRequest, GetByIdContactMessageQueryResponse>
    {
        private readonly IApplicationDbContext? _context;

        public GetByIdContactMessageQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<GetByIdContactMessageQueryResponse> Handle(GetByIdContactMessageQueryRequest request, CancellationToken cancellationToken)
        {

            var contactMessage = _context.ContactMessages.FirstOrDefault(p => p.Id == request.Id);
            return new GetByIdContactMessageQueryResponse
            {
                Id = contactMessage.Id,
                Name = contactMessage.Name,
                Email = contactMessage.Email,
                Phone = contactMessage.Phone,
                Company = contactMessage.Company,
                MessageTitle = contactMessage.MessageTitle,
                MessageBody = contactMessage.MessageBody
            };
        }
    }
}
