using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Services.Models.Banks
{
    public class BankListingServiceModel : BankBaseServiceModel
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string SwiftCode { get; set; }

        public string Id { get; set; }
    }
}
