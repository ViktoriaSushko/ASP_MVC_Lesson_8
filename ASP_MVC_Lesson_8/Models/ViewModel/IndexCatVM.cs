using ASP_MVC_Lesson_8.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_MVC_Lesson_8.Models.ViewModel
{
    public class IndexCatVM
    {
        public IEnumerable<CatDTO>? Cats { get; set; } = default;
        public SelectList? BreedsSL { get; set; }
       public string? Search { get; set; }
        //add pagination
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
