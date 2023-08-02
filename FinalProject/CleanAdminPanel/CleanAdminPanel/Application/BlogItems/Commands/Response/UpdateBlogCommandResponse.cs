using Application.BlogItems.Commands.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Commands.Response
{
    public class UpdateBlogCommandResponse
    {
        public bool IsSuccess { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
