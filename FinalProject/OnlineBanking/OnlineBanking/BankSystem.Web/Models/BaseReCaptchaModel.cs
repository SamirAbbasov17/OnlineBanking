using BankSystem.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Web.Models
{
    public abstract class BaseReCaptchaModel
    {
        [Required]
        [ValidateReCaptcha]
        [BindProperty(Name = "g-recaptcha-response")]
        public string ReCaptchaResponse { get; set; }
    }
}
