using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankAccount;

namespace BankSystem.Web.Models.Account
{
    public class OwnAccountListingViewModel : IMapWith<AccountIndexServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string UniqueId { get; set; }
    }
}
