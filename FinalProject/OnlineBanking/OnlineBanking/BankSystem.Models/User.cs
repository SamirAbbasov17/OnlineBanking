using BankSystem.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string FullName { get; set; }

        public ICollection<Account> BankAccounts { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
