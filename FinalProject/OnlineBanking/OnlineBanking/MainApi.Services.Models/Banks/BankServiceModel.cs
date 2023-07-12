using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Services.Models.Banks
{
    public class BankServiceModel : BankBaseServiceModel
    {
        public string ApiAddress { get; set; }

        public string ApiKey { get; set; }
    }
}
