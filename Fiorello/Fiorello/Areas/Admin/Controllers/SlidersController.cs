using Fiorello.DAL;
using Fiorello.Helper;
using Fiorello.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SlidersController(AppDbContext db, IWebHostEnvironment env)
        {
            _env = env;
            _db= db; 
        }

        public async Task<IActionResult> Index()
        {
            List <Slider> slider = await _db.Sliders.ToListAsync();

            return View(slider);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Slider slider)
        {
            #region Photo
            if(slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can not be null");
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Just select image type");
                return View();
            }

            if (slider.Photo.IsOlder216Kb())
            {
                ModelState.AddModelError("Photo", "Max 216");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "img");
            slider.Image = await slider.Photo.SaveFileAsync(folder);
            #endregion

            await _db.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbslider == null)
                return BadRequest();

            return View(dbslider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
                return NotFound();
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
                return BadRequest();

            #region Photo
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Just select image type");
                    return View();
                }

                if (slider.Photo.IsOlder216Kb())
                {
                    ModelState.AddModelError("Photo", "Max 216");
                    return View();
                }

                string folder = Path.Combine(_env.WebRootPath, "img");
                slider.Image = await slider.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbslider.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                dbslider.Image = slider.Image;
            }
            #endregion

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
    
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
                return BadRequest();

            return View(dbslider);
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbslider == null)
                return BadRequest();

            if (dbslider.IsDeactive)
                dbslider.IsDeactive = false;
            else
                dbslider.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
