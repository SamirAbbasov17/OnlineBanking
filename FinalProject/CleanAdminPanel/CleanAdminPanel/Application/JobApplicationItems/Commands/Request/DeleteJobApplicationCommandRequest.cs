using Application.HelpItems.Commands.Response;
using Application.JobApplicationItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Commands.Request
{
    public class DeleteJobApplicationCommandRequest : IRequest<DeleteJobApplicationCommandResponse>
    {
        public int Id { get; set; }
    }
}
