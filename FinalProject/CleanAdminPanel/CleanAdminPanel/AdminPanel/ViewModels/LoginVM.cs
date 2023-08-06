using System.ComponentModel.DataAnnotations;

namespace AdminPanel.ViewModels
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
