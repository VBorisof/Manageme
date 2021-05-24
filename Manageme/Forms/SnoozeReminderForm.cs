using System;
using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class SnoozeReminderForm
    {
        [Required]
        public long? Id { get; set; }
        [Required]
        public DateTime? Time { get; set; }
    }
}

