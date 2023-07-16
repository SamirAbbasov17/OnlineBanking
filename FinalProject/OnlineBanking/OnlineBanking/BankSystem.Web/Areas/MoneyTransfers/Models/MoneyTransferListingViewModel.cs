using BankSystem.Web.Infrastructure.Collections;

namespace BankSystem.Web.Areas.MoneyTransfers.Models
{
    public class MoneyTransferListingViewModel
    {
        public PaginatedList<MoneyTransferListingDto> MoneyTransfers { get; set; }
    }
}
