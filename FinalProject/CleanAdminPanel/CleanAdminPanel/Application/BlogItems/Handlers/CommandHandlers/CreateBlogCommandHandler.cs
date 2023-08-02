using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Handlers.CommandHandlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommandRequest, CreateBlogCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public CreateBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateBlogCommandResponse> Handle(CreateBlogCommandRequest request, CancellationToken cancellationToken)
        {
            Blog entity = (new()
            {
                Title = request.Title,
                Content = request.Content,
                Description = request.Description,
                Image = request.Image
            });
            _context?.Blogs.Add(entity);
            await _context?.SaveChangesAsync(cancellationToken);
            return new CreateBlogCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
