using BankSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Models.BankAccount
{
    public class AccountCreateServiceModel : AccountBaseServiceModel
    {
        [MaxLength(ModelConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
