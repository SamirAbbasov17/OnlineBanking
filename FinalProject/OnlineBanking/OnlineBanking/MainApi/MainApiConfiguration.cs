using System.ComponentModel.DataAnnotations;

namespace MainApi
{
    public class MainApiConfiguration
    {
        [Required]
        public string Key { get; set; }
    }
}
