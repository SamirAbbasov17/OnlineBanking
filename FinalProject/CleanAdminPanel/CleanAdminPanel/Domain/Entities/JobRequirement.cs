using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobRequirement :  BaseAuditableEntity
    {
        public string Name { get; set; }
        public Job? Job { get; set; }
        public int? JobId { get; set; }
    }
}
