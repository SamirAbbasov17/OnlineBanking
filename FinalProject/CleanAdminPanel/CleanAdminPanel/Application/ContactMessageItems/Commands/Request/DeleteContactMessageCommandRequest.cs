using Application.BlogItems.Commands.Response;
using Application.ContactMessageItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Commands.Request
{
    public class DeleteContactMessageCommandRequest : IRequest<DeleteContactMessageCommandResponse>
    {
        public int Id { get; set; }
    }
}
