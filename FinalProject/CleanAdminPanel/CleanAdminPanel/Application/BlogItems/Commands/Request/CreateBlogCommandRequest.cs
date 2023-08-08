using Application.BlogItems.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Commands.Request
{
    public class CreateBlogCommandRequest : IRequest<CreateBlogCommandResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? RootPath { get;set; } 
    }
}
