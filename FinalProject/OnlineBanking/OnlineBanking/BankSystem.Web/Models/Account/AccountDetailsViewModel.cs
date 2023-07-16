using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Areas.MoneyTransfers.Models;
using BankSystem.Web.Infrastructure.Collections;

namespace BankSystem.Web.Models.Account
{
    public class AccountDetailsViewModel : IMapWith<AccountDetailsServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string UniqueId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserFullName { get; set; }

        public PaginatedList<MoneyTransferListingDto> MoneyTransfers { get; set; }

        public int MoneyTransfersCount { get; set; }

        public string BankName { get; set; }

        public string BankCode { get; set; }

        public string BankCountry { get; set; }
    }
}
