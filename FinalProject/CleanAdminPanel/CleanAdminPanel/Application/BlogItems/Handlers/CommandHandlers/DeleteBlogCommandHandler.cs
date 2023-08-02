using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Handlers.CommandHandlers
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommandRequest, DeleteBlogCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public DeleteBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteBlogCommandResponse> Handle(DeleteBlogCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);
            _context.Blogs.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteBlogCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
