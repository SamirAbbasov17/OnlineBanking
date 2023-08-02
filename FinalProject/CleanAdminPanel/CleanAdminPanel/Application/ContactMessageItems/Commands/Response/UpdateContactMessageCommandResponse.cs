using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactMessageItems.Commands.Response
{
    public class UpdateContactMessageCommandResponse
    {
        public bool IsSuccess { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
    }
}
