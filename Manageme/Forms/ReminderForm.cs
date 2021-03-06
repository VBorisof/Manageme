using System;
using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class ReminderForm
    {
        [Required]
        public long? CategoryId { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public DateTime? Time { get; set; }
    }
}

