using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Models.BankAccount
{
    public class AccountConciseServiceModel:AccountBaseServiceModel
    {
        public string Id { get; set; }

        public string UniqueId { get; set; }

        public decimal Balance { get; set; }

        public string UserFullName { get; set; }
    }
}
