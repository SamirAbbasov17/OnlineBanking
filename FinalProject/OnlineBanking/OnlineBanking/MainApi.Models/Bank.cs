using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Models
{
    public class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SwiftCode { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiAddress { get; set; }

        public string PaymentUrl { get; set; }

        public string CardPaymentUrl { get; set; }

        public string BankIdentificationCardNumbers { get; set; }
    }
}
