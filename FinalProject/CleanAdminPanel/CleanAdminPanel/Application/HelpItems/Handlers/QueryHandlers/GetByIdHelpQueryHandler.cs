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
    public class GetByIdHelpQueryHandler : IRequestHandler<GetByIdHelpQueryRequest, GetByIdHelpQueryResponse>
    {
        private readonly IApplicationDbContext? _context;

        public GetByIdHelpQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<GetByIdHelpQueryResponse> Handle(GetByIdHelpQueryRequest request, CancellationToken cancellationToken)
        {

            var Help = _context.Helps.FirstOrDefault(p => p.Id == request.Id);
            return new GetByIdHelpQueryResponse
            {
                Id = Help.Id,
                Name = Help.Name,
                Description = Help.Description,
                Content = Help.Content
            };
        }
    }
}
