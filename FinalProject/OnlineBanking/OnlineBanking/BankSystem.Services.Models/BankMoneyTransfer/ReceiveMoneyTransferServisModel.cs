using BankSystem.Common;
using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Models.BankMoneyTransfer
{
    public class ReceiveMoneyTransferServisModel : MoneyTransferBaseServiceModel
    {
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.MoneyTransfer.MinStartingPrice, ModelConstants.MoneyTransfer.MaxStartingPrice)]
        public decimal Amount { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public DateTime MadeOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string Source => this.Destination;

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string Destination { get; set; }
    }
}
