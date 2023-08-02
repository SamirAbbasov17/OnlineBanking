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
    public class GetAllBlogQueryHandler : IRequestHandler<GetAllBlogQueryRequest, List<GetAllBlogQueryResponse>>
    {
        private readonly IApplicationDbContext? _context;

        public GetAllBlogQueryHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<List<GetAllBlogQueryResponse>> Handle(GetAllBlogQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context?.Blogs != null)
            {
                return _context.Blogs.Select(product => new GetAllBlogQueryResponse
                {
                    Id = product.Id,
                    Title = product.Title,
                    Content = product.Content,
                    Description = product.Description,
                    Image = product.Image
                }).ToList();
            }
            else
            {
                return new List<GetAllBlogQueryResponse>();
            }
        }
    }
}
