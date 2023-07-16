using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankAccount;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Models.Account
{
    public class AccountCreateBindingModel : IMapWith<AccountCreateServiceModel>
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
