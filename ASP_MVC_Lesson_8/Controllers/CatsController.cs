using ASP_MVC_Lesson_8.Data;
using ASP_MVC_Lesson_8.Models;
using ASP_MVC_Lesson_8.Models.DTOs;
using ASP_MVC_Lesson_8.Models.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC_Lesson_8.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatsContext _context;
        private readonly ILogger<CatsController> _logger;
        private readonly IMapper mapper;

        public CatsController(ILogger<CatsController> logger, CatsContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int? breedId, string? search, int page=1)
        {
            //add pagination
            int itemsPerPage = 3;
            var cats = _context.Cats.Include(c => c.Breed).Where(c=>!c.IsDeleted);
            int totalPage =(int)(Math.Ceiling((decimal)cats.ToList().Count() / itemsPerPage));
            cats = cats.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);


            if(breedId is not null && breedId.Value != 0)
            {
                cats = cats.Where(c => c.BreedId == breedId);
            }
            if(search is not null)
            {
                cats = cats.Where(c => c.CatName.Contains(search));
            }
            var breeds = await _context.Breeds.Distinct().ToListAsync();
            IEnumerable<CatDTO>? catDTOs = mapper.Map<IEnumerable<CatDTO>>(cats);
            IndexCatVM catVM = new IndexCatVM
            {
                //Cats = cats,
                Cats = catDTOs,
                BreedsSL = new SelectList(breeds, "Id", "BreedName"),
                Search = search,
                //add pagination
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                PageCount = totalPage
            };
            return View(catVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();
            var cat = await _context.Cats.Include(c => c.Breed).FirstOrDefaultAsync(c => c.Id == id);
            if (cat is null)
                return NotFound();
            CatDTO? catDTO = mapper.Map<CatDTO>(cat); 
            return View(catDTO);
        }

        public async Task<IActionResult> Create()
        {
            CreateCatVM catVM = new CreateCatVM()
            {
                Breeds = new SelectList(await _context.Breeds.ToListAsync(), "Id", "BreedName"),
                CatsGenders = new SelectList(Enum.GetNames<CatsGender>())
            };
            return View(catVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatDTO cat, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                //Cat createDTO = new Cat
                //{
                //    CatName = cat.CatName,
                //    Description = cat.Description,
                //    Gender = cat.Gender,
                //    IsVacinated = cat.IsVacinated,
                //    BreedId = cat.BreedId

                //};
                Cat? createDTO = mapper.Map<Cat>(cat);
                if (imageFile != null && imageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await imageFile.CopyToAsync(ms);
                    createDTO.Image = ms.ToArray();
                }

                _context.Cats.Add(createDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateCatVM catVM = new CreateCatVM()
            {
                Breeds = new SelectList(await _context.Breeds.ToListAsync(), "Id", "BreedName", cat.BreedId),

                CatsGenders = new SelectList(Enum.GetNames<CatsGender>(), cat.Gender),
                Cat = cat
            };
           
            return View(catVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var cat = await _context.Cats.FindAsync(id);
            if (cat is null)
                return NotFound();
            EditCatVM catVM = new EditCatVM
            {
                Cat=mapper.Map<CatDTO>(cat),
                Breeds = new SelectList(await _context.Breeds.ToListAsync(), "Id", "BreedName", cat.BreedId),
                CatsGenders = new SelectList(Enum.GetNames<CatsGender>(), cat.Gender)
            };
          
            return View(catVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CatDTO cat, IFormFile? imageFile)
        {
            if (id != cat.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                Cat? editCat = mapper.Map<Cat>(cat);
                if (editCat == null)
                    return NotFound();

                if (imageFile != null && imageFile.Length > 0)
                {
                   using(BinaryReader reader=new BinaryReader(imageFile.OpenReadStream()))
                    {
                        editCat.Image = reader.ReadBytes((int)imageFile.Length);
                    }                    
                }
                try
                {
                    _context.Update(editCat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!CatExists(cat.Id))
                        return NotFound();
                    else
                        throw;
                }
               return RedirectToAction(nameof(Index));
            }
            EditCatVM catVM = new EditCatVM()
            {
                Cat = mapper.Map<CatDTO>(cat),
                Breeds = new SelectList(await _context.Breeds.ToListAsync(), "Id", "BreedName", cat.BreedId),
                CatsGenders = new SelectList(Enum.GetNames<CatsGender>(), cat.Gender)
            };
            return View(catVM);
        }

        private bool CatExists(int id)
        {
            return _context.Cats.Any(c => c.Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var cat = await _context.Cats.Include(c => c.Breed).FirstOrDefaultAsync(c => c.Id == id);
            if (cat is null)
                return NotFound();
            CatDTO? catDTO = mapper.Map<CatDTO>(cat);
            return View(catDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id is null)
                return NotFound();
            var cat = await _context.Cats.FindAsync(id);
            if (cat is null)
                return NotFound();
            //_context.Cats.Remove(cat);
            cat.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
