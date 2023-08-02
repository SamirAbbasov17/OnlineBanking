using Application.BlogItems.Queries.Request;
using Application.BlogItems.Queries.Response;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Handlers.QueryHandlers
{
    public class GetByIdBlogQueryHandler : IRequestHandler<GetByIdBlogQueryRequest, GetByIdBlogQueryResponse>
    {
        private readonly IApplicationDbContext? _context;

        public GetByIdBlogQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<GetByIdBlogQueryResponse> Handle(GetByIdBlogQueryRequest request, CancellationToken cancellationToken)
        {

            var product = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);
            return new GetByIdBlogQueryResponse
            {
                Id = product.Id,
                Title = product.Title,
                Content = product.Content,
                Description = product.Description,
                Image = product.Image
            };
        }
    }
}
