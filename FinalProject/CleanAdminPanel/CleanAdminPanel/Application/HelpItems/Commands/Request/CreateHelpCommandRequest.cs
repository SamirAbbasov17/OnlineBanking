using Application.HelpItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HelpItems.Commands.Request
{
    public class CreateHelpCommandRequest : IRequest<CreateHelpCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
