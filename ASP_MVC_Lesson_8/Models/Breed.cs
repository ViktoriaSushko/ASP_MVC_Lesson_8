using System.ComponentModel.DataAnnotations;

namespace ASP_MVC_Lesson_8.Models
{
    public class Breed
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Breed name")]
        public string BreedName { get; set; }
        public virtual ICollection<Cat>? Cats { get; set; } = default!;
    }
}
