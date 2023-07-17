using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankCard;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Areas.Cards.Models
{
    public class CardCreateViewModel : IMapWith<CardCreateServiceModel>
    {
        public IEnumerable<SelectListItem>? BankAccounts { get; set; } 

        [Required]
        [Display(Name = "Choose account")]
        public string AccountId { get; set; }
    }
}
