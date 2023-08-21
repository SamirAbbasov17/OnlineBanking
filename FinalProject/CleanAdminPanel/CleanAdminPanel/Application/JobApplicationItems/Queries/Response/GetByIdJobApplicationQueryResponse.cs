using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplicationItems.Queries.Response
{
    public class GetByIdJobApplicationQueryResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Linkedin { get; set; }
        public string? Cv { get; set; }
    }
}
