using BankSystem.Web.Areas.MoneyTransfers.Models;
using BankSystem.Web.Models.Account;

namespace BankSystem.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<AccountIndexViewModel> UserBankAccounts { get; set; }

        public IEnumerable<MoneyTransferListingDto> MoneyTransfers { get; set; }
    }
}
