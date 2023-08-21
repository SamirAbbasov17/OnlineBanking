using Application.JobApplicationItems.Commands.Response;
using Application.JobItems.Commands.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Commands.Request
{
    public class UpdateJobCommandRequest : IRequest<UpdateJobCommandResponse>
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTime { get; set; }
        public string? Experience { get; set; }
        public List<JobRequirement>? JobRequirementList { get; set; }
    }
}
