using BankSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Areas.MoneyTransfers.Models.Global.Create
{
    public class GlobalMoneyTransferCreateDestinationAccountDto
    {
        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        [Display(Name = "Account/IBAN")]
        public string UniqueId { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        [Display(Name = "Beneficiary's name")]
        public string UserFullName { get; set; }
    }
}
