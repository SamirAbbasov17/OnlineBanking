using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.Common.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using MediatR;
using Newtonsoft.Json;
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
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");

        public DeleteBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<DeleteBlogCommandResponse> Handle(DeleteBlogCommandRequest request, CancellationToken cancellationToken)
        {


            var deleteProduct = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);
            var storage = StorageClient.Create(google);
            var folderPath = Path.Combine(request.RootPath, "uploads");
            var path = Path.Combine(folderPath, deleteProduct.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                storage.DeleteObject("clean_admin", deleteProduct.Image);
            }

            _context.Blogs.Remove(deleteProduct);
            await _context?.SaveChangesAsync(cancellationToken);
            return new DeleteBlogCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
