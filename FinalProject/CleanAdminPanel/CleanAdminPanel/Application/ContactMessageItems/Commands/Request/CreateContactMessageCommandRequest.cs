using Application.ContactMessageItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Commands.Request
{
    public class CreateContactMessageCommandRequest : IRequest<CreateContactMessageCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
    }
}
