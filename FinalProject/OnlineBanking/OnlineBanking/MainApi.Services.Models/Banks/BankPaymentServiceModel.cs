using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Services.Models.Banks
{
    public class BankPaymentServiceModel : BankBaseServiceModel
    {
        public string ApiKey { get; set; }

        public string PaymentUrl { get; set; }

        public string CardPaymentUrl { get; set; }
    }
}
