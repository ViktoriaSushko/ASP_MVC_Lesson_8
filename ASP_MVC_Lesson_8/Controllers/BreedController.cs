using ASP_MVC_Lesson_8.Data;
using ASP_MVC_Lesson_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC_Lesson_8.Controllers
{
    public class BreedController : Controller
    {
        private readonly CatsContext context;
        private readonly ILogger<HomeController> logger;
        public BreedController(ILogger<HomeController> logger, CatsContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Breed> breeds = context.Breeds;
            return View(await breeds.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string? breedname)
        {
            if(breedname is null)
            {
                ModelState.AddModelError(string.Empty, "Breed name field couldn't be empty!");
            }
            if(context.Breeds.SingleOrDefault(b=>b.BreedName==breedname)is null)
            {
                Breed breed = new Breed { BreedName = breedname };
                await context.Breeds.AddAsync(breed);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();
            Breed? breed = await context.Breeds.FindAsync(id);
            if(breed is null)
                return NotFound();
            return View(breed);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            Breed? breed = await context.Breeds.FindAsync(id);
            if (breed is null)
                return NotFound();
            return View(breed);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id is null)
                return NotFound();
            Breed? breed = await context.Breeds.FindAsync(id);
            if (breed is null)
                return NotFound();
            context.Breeds.Remove(breed);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();
            Breed? breed = await context.Breeds.FindAsync(id);
            if (breed is null)
                return NotFound();
            return View(breed);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Breed breed)
        {
            if (ModelState.IsValid)
            {
                Breed? editBreed = await context.Breeds.FindAsync(breed.Id);

                if (editBreed is null)
                {
                    ModelState.AddModelError(string.Empty, "Breed name field couldn't be empty!");
                }
                editBreed.BreedName = breed.BreedName;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(breed);
            }
         
            
        }
    }
}
