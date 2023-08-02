using Application.Common.Interfaces;
using Application.HelpItems.Queries.Request;
using Application.HelpItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Handlers.QueryHandlers
{
    public class GetAllHelpQueryHandler : IRequestHandler<GetAllHelpQueryRequest, List<GetAllHelpQueryResponse>>
    {
        private readonly IApplicationDbContext? _context;

        public GetAllHelpQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<List<GetAllHelpQueryResponse>> Handle(GetAllHelpQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context?.Helps != null)
            {
                return _context.Helps.Select(request => new GetAllHelpQueryResponse
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Content = request.Content
                }).ToList();
            }
            else
            {
                return new List<GetAllHelpQueryResponse>();
            }
        }
    }
}
