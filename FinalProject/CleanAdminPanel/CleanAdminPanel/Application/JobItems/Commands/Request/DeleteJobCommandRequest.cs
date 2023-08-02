using Application.JobApplicationItems.Commands.Response;
using Application.JobItems.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Commands.Request
{
    public class DeleteJobCommandRequest : IRequest<DeleteJobCommandResponse>
    {
        public int Id { get; set; }
    }
}
