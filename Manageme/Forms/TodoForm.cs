using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class TodoForm
    {
        [Required]
        public long? CategoryId { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}

