using BankSystem.Web.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Models
{
    public class PaymentConfirmBindingModel
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string DestinationBankName { get; set; }

        public string DestinationBankCountry { get; set; }

        public string DestinationBankAccountUniqueId { get; set; }

        public string RecipientName { get; set; }

        public IEnumerable<OwnAccountListingViewModel> OwnAccounts { get; set; }

        [Required]
        public string DataHash { get; set; }

        [Required]
        [Display(Name = "Source account")]
        public string AccountId { get; set; }
    }
}
