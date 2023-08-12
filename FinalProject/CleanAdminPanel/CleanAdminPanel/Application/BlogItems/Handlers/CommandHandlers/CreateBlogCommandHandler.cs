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
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Application.BlogItems.Handlers.CommandHandlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommandRequest, CreateBlogCommandResponse>
    {
        private readonly IApplicationDbContext? _context;
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");

        public CreateBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<CreateBlogCommandResponse> Handle(CreateBlogCommandRequest request,CancellationToken cancellationToken)
        {
            var storage = StorageClient.Create(google);
            var bucket = storage.GetBucket("clean_admin");
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(request.Image.FileName);
            var folderPath = Path.Combine(request.RootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(fileStream);
            }
            using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
            {
                string objectName = Path.GetFileName(filePath);
                storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
            }


            Blog entity = (new()
            {
                Title = request.Title,
                Content = request.Content,
                Description = request.Description,
                Image = fileName,
                Filter = request.Filter
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
