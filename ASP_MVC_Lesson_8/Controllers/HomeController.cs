using ASP_MVC_Lesson_8.Data;
using ASP_MVC_Lesson_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_MVC_Lesson_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatsContext context;
        private readonly ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> logger,CatsContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Cat> cats = context.Cats.Include(c => c.Breed);
            return View(await cats.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
