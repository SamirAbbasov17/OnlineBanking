using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.BlogItems.Queries.Response;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Handlers.CommandHandlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommandRequest, UpdateBlogCommandResponse>
    {
        private readonly IApplicationDbContext? _context;

        public UpdateBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateBlogCommandResponse> Handle(UpdateBlogCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Content == null)
            {
                var blog = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);
                return new UpdateBlogCommandResponse
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    Description = blog.Description,
                    Image = blog.Image
                };
            }
            var updateProduct = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);

            updateProduct.Title = request.Title;
            updateProduct.Content = request.Content;
            updateProduct.Description = request.Description;
            updateProduct.Image = request.Image;
            _context.Blogs.Update(updateProduct);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateBlogCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
