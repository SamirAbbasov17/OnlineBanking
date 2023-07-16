using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Common;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Areas.MoneyTransfers.Models.Global.Create;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Areas.MoneyTransfers.Models.Global
{
    public class GlobalMoneyTransferCentralApiBindingModel : IMapWith<GlobalMoneyTransferCreateBindingModel>,
        IMapWith<MoneyTransferCreateServiceModel>
    {
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

        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string SenderAccountUniqueId { get; set; }
    }
}
