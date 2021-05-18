using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class LoginForm
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

