using Application.ContactMessageItems.Commands.Response;
using Application.HelpItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Commands.Request
{
    public class UpdateHelpCommandRequest : IRequest<UpdateHelpCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
