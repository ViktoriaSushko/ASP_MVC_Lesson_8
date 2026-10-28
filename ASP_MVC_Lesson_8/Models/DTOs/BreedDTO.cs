using System.ComponentModel.DataAnnotations;

namespace ASP_MVC_Lesson_8.Models.DTOs
{
    public class BreedDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Breed name")]
        public string BreedName { get; set; }
        public virtual ICollection<CatDTO>? Cats { get; set; } = default!;
    }
}
