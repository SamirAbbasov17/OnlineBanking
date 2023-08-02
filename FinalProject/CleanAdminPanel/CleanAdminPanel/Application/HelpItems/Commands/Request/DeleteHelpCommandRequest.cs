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
    public class DeleteHelpCommandRequest : IRequest<DeleteHelpCommandResponse>
    {
        public int Id { get; set; }
    }
}
