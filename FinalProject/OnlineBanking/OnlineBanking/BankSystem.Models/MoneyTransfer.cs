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
    public class MoneyTransfer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime MadeOn { get; set; }

        [Required]
        public string AccountId { get; set; }

        public Account Account { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string Source { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string Destination { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }
    }
}
