using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Common.EmailSender
{
    public class SendGridConfiguration
    {
        [Required]
        public string ApiKey { get; set; }
    }
}
