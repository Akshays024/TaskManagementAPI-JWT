using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class LOgin
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
