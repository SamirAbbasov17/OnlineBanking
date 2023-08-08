using Application.BlogItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Commands.Request
{
    public class DeleteBlogCommandRequest : IRequest<DeleteBlogCommandResponse>
    {
        public int Id { get; set; }
        public string? RootPath { get; set; }
    }
}
}
