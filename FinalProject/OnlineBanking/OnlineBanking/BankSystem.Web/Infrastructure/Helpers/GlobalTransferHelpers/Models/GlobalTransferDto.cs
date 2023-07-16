using BankSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers.Models
{
    public class GlobalTransferDto
    {
        [Required]
        public string SourceAccountId { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.MoneyTransfer.MinStartingPrice, ModelConstants.MoneyTransfer.MaxStartingPrice)]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.SwiftCodeMaxLength)]
        public string DestinationBankSwiftCode { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.NameMaxLength)]
        public string DestinationBankName { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.CountryMaxLength)]
        public string DestinationBankCountry { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string DestinationBankAccountUniqueId { get; set; }
    }
}
