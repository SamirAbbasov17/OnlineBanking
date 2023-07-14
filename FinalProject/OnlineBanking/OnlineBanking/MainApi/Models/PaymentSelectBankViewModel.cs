namespace MainApi.Models
{
    public class PaymentSelectBankViewModel
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public IEnumerable<BankListingViewModel> Banks { get; set; }
    }
}
