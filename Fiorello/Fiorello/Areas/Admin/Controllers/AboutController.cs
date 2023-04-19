using Fiorello.DAL;
using Fiorello.Helper;
using Fiorello.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext db,IWebHostEnvironment env)
        {
            _env = env;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            About about = await _db.About.FirstOrDefaultAsync();
            return View(about);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            About about = await _db.About.FirstOrDefaultAsync(x=>x.Id == id);
            if (about == null)
                return BadRequest();

            return View(about);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            About dbabout = await _db.About.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbabout==null)
                return BadRequest();

            return View(dbabout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,About about)
        {
            if (id == null)
                return NotFound();
            About dbabout = await _db.About.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbabout==null)
                return BadRequest();

            #region Photo
            if(about.Photo != null)
            {
                if (!about.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Just Select image type!");
                    return View();
                }

                if(about.Photo.IsOlder216Kb())
                {
                    ModelState.AddModelError("Photo", "Max 216Kb");
                    return View();
                }

                string folder = Path.Combine(_env.WebRootPath, "img");
                about.Image = await about.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbabout.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbabout.Image = about.Image;
            }
            #endregion

            dbabout.Title = about.Title;
            dbabout.Description = about.Description;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
