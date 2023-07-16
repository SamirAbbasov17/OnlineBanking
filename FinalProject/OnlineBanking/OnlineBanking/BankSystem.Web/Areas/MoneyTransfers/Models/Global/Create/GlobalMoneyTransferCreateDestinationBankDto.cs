using BankSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Areas.MoneyTransfers.Models.Global.Create
{

    public class GlobalMoneyTransferCreateDestinationBankDto
    {
        [Required]
        [MaxLength(ModelConstants.Account.SwiftCodeMaxLength)]
        [Display(Name = "Swift/Bank code")]
        public string SwiftCode { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.CountryMaxLength)]
        public string Country { get; set; }

        [Required]
        public GlobalMoneyTransferCreateDestinationAccountDto Account { get; set; }
    }
}
