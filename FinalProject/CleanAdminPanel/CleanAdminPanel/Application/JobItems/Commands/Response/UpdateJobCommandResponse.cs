using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobItems.Commands.Response
{
    public class UpdateJobCommandResponse
    {
        public bool IsSuccess { get; set; }
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTime { get; set; }
        public List<JobRequirement>? JobRequirementList { get; set; }
    }
}
