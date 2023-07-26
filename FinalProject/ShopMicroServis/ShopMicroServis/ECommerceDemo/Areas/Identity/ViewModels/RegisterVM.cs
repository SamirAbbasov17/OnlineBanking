using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Areas.Identity.ViewModels { 
    public class RegisterVM
    {
        public string Username { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set;} = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
