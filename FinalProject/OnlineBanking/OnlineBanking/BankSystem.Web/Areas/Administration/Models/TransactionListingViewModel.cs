using BankSystem.Web.Areas.MoneyTransfers.Models;

namespace BankSystem.Web.Areas.Administration.Models
{
    public class TransactionListingViewModel
    {
        public IEnumerable<MoneyTransferListingDto> MoneyTransfers { get; set; }

        public string ReferenceNumber { get; set; }
    }
}
