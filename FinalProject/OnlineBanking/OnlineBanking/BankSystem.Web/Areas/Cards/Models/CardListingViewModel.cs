using BankSystem.Web.Infrastructure.Collections;

namespace BankSystem.Web.Areas.Cards.Models
{
    public class CardListingViewModel
    {
        public PaginatedList<CardListingDto> Cards { get; set; }
    }
}
