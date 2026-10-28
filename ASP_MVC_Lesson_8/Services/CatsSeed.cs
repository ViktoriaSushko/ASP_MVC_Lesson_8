using ASP_MVC_Lesson_8.Data;
using ASP_MVC_Lesson_8.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_MVC_Lesson_8.Services
{
    public static class CatsSeed
    {
        public static async Task SeedData(IServiceProvider serviceProvider, IWebHostEnvironment environment)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<CatsContext>>();
            using(CatsContext context=new CatsContext(options))
            {
                if (context.Breeds.Any())
                    return;
                Breed breed1 = new Breed { BreedName = "Scottish Straight" };
                Breed breed2 = new Breed { BreedName = "Maine Coon" };
                Breed breed3 = new Breed { BreedName = "Ukrainian Levkoy" };
                Breed breed4 = new Breed { BreedName = "Abessin" };
                Breed breed5 = new Breed { BreedName = "Sphinx" };
                string filename1 = "scotish-breed.png";
                string filename2 = "maine-coon.jpg";
                string filename3 = "bashira_naked.jpg";
                string filename4 = "abessin.jpg";
                string filename5 = "sphynx.jpg";
                string filename6 = "caratteristiche-maine-coon.jpg";
                byte[] image1 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath,"images",filename1));
                byte[] image2 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "images", filename2)); 
                byte[] image3 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "images", filename3));
                byte[] image4 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "images", filename4));
                byte[] image5 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "images", filename5));
                byte[] image6 = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "images", filename6));
                string lorem1 = "Lorem ipsum dolor sit amet consectetur adipisicing elit." +
                                    " Nemo ratione dolores vitae quo optio perferendis eligendi eius laboriosam eum. " +
                                    "Exercitationem deleniti eligendi quia sed distinctio architecto ex. Repellat, eligendi ut!";
                string lorem2 = "Commodi placeat exercitationem perferendis omnis! Provident praesentium quod" +
                                    " voluptate adipisci cumque, id eligendi porro maiores. Repudiandae eveniet velit eos! " +
                                    "Quis fugiat temporibus placeat cupiditate enim, veniam illo aperiam ea eaque";
                string lorem3 = "Facilis fuga, dolor doloremque laborum impedit placeat maxime nesciunt " +
                                    "omnis odit ad illo. Eos doloremque et maxime corrupti suscipit fuga saepe alias ut. Ad, " +
                                    "a necessitatibus! Alias praesentium debitis velit";
                Cat cat1 = new Cat
                {
                    CatName = "Vasyl",
                    Description = lorem1,
                    //Gender = CatsGender.Male,
                    Image = image1,
                    IsVacinated = false,
                    IsDeleted = false,
                    Breed = breed1
                };
                Cat cat2 = new Cat
                {
                    CatName = "Murchik",
                    Description = lorem2,
                    Gender=CatsGender.Male.ToString(),
                    //Gender = CatsGender.Male,
                    Image = image2,
                    IsVacinated = false,
                    IsDeleted = false,
                    Breed = breed2
                };
                Cat cat3 = new Cat
                {
                    CatName = "Mavka",
                    Description = lorem3,
                    Gender = CatsGender.Female.ToString(),
                    //Gender = CatsGender.Female,
                    Image = image3,
                    IsVacinated = true,
                    IsDeleted = false,
                    Breed = breed3
                };
                Cat cat4 = new Cat
                {
                    CatName = "Vitaminka",
                    Description = lorem1,
                    Gender = CatsGender.Female.ToString(),
                    //Gender = CatsGender.Female,
                    Image = image4,
                    IsVacinated = true,
                    IsDeleted = false,
                    Breed = breed4
                };
                Cat cat5 = new Cat
                {
                    CatName = "Imkhotep",
                    Description = lorem2,
                    Gender = CatsGender.Female.ToString(),
                    //Gender = CatsGender.Female,
                    Image = image5,
                    IsVacinated = true,
                    IsDeleted = false,
                    Breed = breed5
                };
                Cat cat6 = new Cat
                {
                    CatName = "Imkhotep",
                    Description = lorem3,
                    Gender = CatsGender.Female.ToString(),
                    //Gender = CatsGender.Female,
                    Image = image6,
                    IsVacinated = true,
                    IsDeleted = false,
                    Breed = breed2
                };
                context.Breeds.AddRange(breed1,breed2,breed3,breed4,breed5);

                context.Cats.AddRange(cat1,cat2,cat3,cat4,cat5,cat6);

                await context.SaveChangesAsync();

            }
        }
    }
}
