using Application.BlogItems.Commands.Request;
using Application.BlogItems.Commands.Response;
using Application.BlogItems.Queries.Response;
using Application.Common.Interfaces;
using Application.ContactMessageItems.Commands.Response;
using Domain.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");

        public UpdateBlogCommandHandler(IApplicationDbContext? context)
        {
            _context = context;
        }

        public async Task<UpdateBlogCommandResponse> Handle(UpdateBlogCommandRequest request, CancellationToken cancellationToken)
        {
            var blog = _context.Blogs.FirstOrDefault(p => p.Id == request.Id);
            if (request.Content == null)
            {
            
                    var ContactMessage = _context.ContactMessages.FirstOrDefault(p => p.Id == request.Id);
                return new UpdateBlogCommandResponse
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    Description = blog.Description,
                    Image = blog.Image,
                    Filter = blog.Filter
                };

            }

            if (request.Image != null)
            {
                var storage = StorageClient.Create(google);
                var bucket = storage.GetBucket("clean_admin");
                var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(request.Image.FileName);
                var folderPath = Path.Combine(request.RootPath, "uploads");
                var filePath = Path.Combine(folderPath, fileName);
                var deletePath = Path.Combine(folderPath, blog.Image);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                    storage.DeleteObject("clean_admin", blog.Image);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }
                using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
                {
                    string objectName = Path.GetFileName(filePath);
                    storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
                }

                blog.Title = request.Title;
                blog.Content = request.Content;
                blog.Description = request.Description;
                blog.Image = fileName;
                blog.Filter = request.Filter;
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync(cancellationToken);
                return new UpdateBlogCommandResponse
                {
                    IsSuccess = true,
                };
            
            }
            else
            {
                blog.Title = request.Title;
                blog.Content = request.Content;
                blog.Description = request.Description;
                blog.Filter = request.Filter;
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync(cancellationToken);
                return new UpdateBlogCommandResponse
                {
                    IsSuccess = true,
                };
            
            }
              
        }
    }
}
