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
    public class GetAllContactMessageQueryHandler : IRequestHandler<GetAllContactMessageQueryRequest, List<GetAllContactMessageQueryResponse>>
    {
        private readonly IApplicationDbContext? _context;

        public GetAllContactMessageQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<List<GetAllContactMessageQueryResponse>> Handle(GetAllContactMessageQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context?.ContactMessages != null)
            {
                return _context.ContactMessages.Select(request => new GetAllContactMessageQueryResponse
                {
                    Id = request.Id,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Company = request.Company,
                    MessageTitle = request.MessageTitle,
                    MessageBody = request.MessageBody
                }).ToList();
            }
            else
            {
                return new List<GetAllContactMessageQueryResponse>();
            }
        }
    }
}
