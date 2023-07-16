using BankSystem.Web.Infrastructure.Collections;

namespace BankSystem.Web.Areas.Administration.Models
{
    public class AllAccountsListViewModel
    {
        public PaginatedList<AccountListingViewModel> BankAccounts { get; set; }
    }
}
