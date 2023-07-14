using BankSystem.Common.AutoMapping.Interfaces;
using MainApi.Services.Models.Banks;

namespace MainApi.Models
{
    public class BankListingViewModel : IMapWith<BankListingServiceModel>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string SwiftCode { get; set; }

        public string Id { get; set; }
    }
}
