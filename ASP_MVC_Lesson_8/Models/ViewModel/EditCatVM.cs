using ASP_MVC_Lesson_8.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_MVC_Lesson_8.Models.ViewModel
{
    public class EditCatVM
    {
        public CatDTO? Cat { get; set; }
        public SelectList? Breeds { get; set; }
        public SelectList? CatsGenders { get; set; }
    }
}
