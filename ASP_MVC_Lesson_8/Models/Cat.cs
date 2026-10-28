using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_MVC_Lesson_8.Models
{
    public enum CatsGender
    {
        Male,Female
    }
    public class Cat
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Cat's name")]
        public string CatName { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Vaccination status")]
        public bool IsVacinated { get; set; }
        [Display(Name ="Is deleted entity")]
        public bool IsDeleted { get; set; }
        [Display(Name = "Cat's photo")]
        public byte[]? Image { get; set; }
        [Display(Name = "Cat's gender")]
        //public string Gender { get; set; }
        public CatsGender Gender { get; set; }
        [ForeignKey(nameof(Breed))]
        public int BreedId { get; set; } = default!;
        public virtual Breed Breed { get; set; } = default!;
    }
}
