using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Common;

namespace BankSystem.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string UniqueId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Card> Cards { get; set; }

        public ICollection<MoneyTransfer> Transfers { get; set; }
    }
}
