using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class RegistrationForm
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

