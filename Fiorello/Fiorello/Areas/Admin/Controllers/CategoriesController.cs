using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Category category)
        {
            bool isExist = await _db.Categories.AnyAsync(x=>x.Name==category.Name);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This Category Name already is exist !");
                return View();
            }

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x=>x.Id==Id);
            if (dbcategory == null)
                return BadRequest();

            return View(dbcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? Id,Category category)
        {
            if (Id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (dbcategory == null)
                return BadRequest();

            bool isExist = await _db.Categories.AnyAsync(x=>x.Name==category.Name && x.Id!=Id);
           if(isExist)
            {
             ModelState.AddModelError("Name","This Category Name already is Exist !");
                return View();
            }


            dbcategory.Name = category.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (dbcategory == null)
                return BadRequest();

            return View(dbcategory);
        }

        public async Task<IActionResult> Activity(int? Id)
        {
            if (Id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (dbcategory == null)
                return BadRequest();

            if (dbcategory.IsDeactive)
            {
                dbcategory.IsDeactive = false;
            }
            else
            {
                dbcategory.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
