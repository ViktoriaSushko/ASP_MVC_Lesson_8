using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_MVC_Lesson_8.Models.DTOs
{
    public class CatDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cat's name")]
        public string CatName { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Vaccination status")]
        public bool IsVacinated { get; set; }
        
        [Display(Name = "Cat's photo")]
        public byte[]? Image { get; set; }
        [Display(Name = "Cat's gender")]
        //public CatsGender Gender { get; set; }
        public string Gender { get; set; }
        //[ForeignKey(nameof(Breed))]
        public int BreedId { get; set; } = default!;
        [Display(Name = "Breed Name")]
        public BreedDTO? Breed { get; set; }
    }
}
