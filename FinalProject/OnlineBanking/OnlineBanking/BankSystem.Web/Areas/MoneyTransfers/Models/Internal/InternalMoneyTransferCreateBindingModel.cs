using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Common;
using BankSystem.Services.Models.BankMoneyTransfer;
using System.ComponentModel.DataAnnotations;
using BankSystem.Services.BankAccount;
using BankSystem.Web.Models.Account;

namespace BankSystem.Web.Areas.MoneyTransfers.Models.Internal
{
    public class InternalMoneyTransferCreateBindingModel : IMapWith<MoneyTransferCreateServiceModel>, IValidatableObject
    {
        private const string DestinationAccountIncorrectError =
            "Destination account is incorrect or belongs to a different bank";

        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.MoneyTransfer.MinStartingPrice,
            ModelConstants.MoneyTransfer.MaxStartingPrice, ErrorMessage =
                "The amount cannot be lower than 0.01")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Destination account")]
        [RegularExpression(@"^[A-Z]{4}\d{8}$", ErrorMessage = DestinationAccountIncorrectError)]
        public string DestinationAccountUniqueId { get; set; }

        public IEnumerable<OwnAccountListingViewModel>? OwnAccounts { get; set; }

        [Required]
        [Display(Name = "Source account")]
        public string AccountId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var uniqueIdHelper = validationContext.GetService<IAccountUniqueIdHelper>();
            if (!uniqueIdHelper.IsUniqueIdValid(this.DestinationAccountUniqueId))
            {
                yield return new ValidationResult(DestinationAccountIncorrectError,
                    new[] { nameof(this.DestinationAccountUniqueId) });
            }
        }
    }
}
