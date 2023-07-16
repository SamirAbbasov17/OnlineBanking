using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankAccount;

namespace BankSystem.Web.Areas.Administration.Models
{
    public class AccountListingViewModel : IMapWith<AccountDetailsServiceModel>
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string UniqueId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string UserFullName { get; set; }
    }
}
