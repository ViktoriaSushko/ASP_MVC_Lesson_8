using ASP_MVC_Lesson_8.Models;
using ASP_MVC_Lesson_8.Models.DTOs;
using AutoMapper;

namespace ASP_MVC_Lesson_8.AutoMapperProfiles
{
    public class CatProfile:Profile
    {
        public CatProfile()
        {
            CreateMap<Cat, CatDTO>().ReverseMap();
        }
    }
}
