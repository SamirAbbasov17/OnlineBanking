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
    public class UpdateJobApplicationCommandRequest : IRequest<UpdateJobApplicationCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Linkedin { get; set; }
        public string Cv { get; set; }
    }
}
