using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Job : BaseAuditableEntity
    {
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTime { get; set; }
        public List<JobRequirement>? JobRequirementList { get; set; }

    }
}
