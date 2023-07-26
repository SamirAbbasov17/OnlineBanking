using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Areas.Identity.ViewModels
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
