using System.ComponentModel.DataAnnotations;

namespace Manageme.Forms
{
    public class CreateCategoryForm
    {
        [Required]
        public string Name { get; set; }

        public CreateCategoryForm(string name)
        {
            Name = name;
        }
    }
}

