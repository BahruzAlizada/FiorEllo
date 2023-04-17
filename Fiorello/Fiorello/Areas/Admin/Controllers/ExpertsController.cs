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
    public class ExpertsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ExpertsController(AppDbContext db,IWebHostEnvironment env)
        {
            _env = env;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Expert> experts = await _db.Experts.Include(x => x.Position).ToListAsync();
            return View(experts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Expert expert,int positionId)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            #region Photo
            if(expert.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can not be null");
                return View();
            }

            if (!expert.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Just selecect image type");
                return View();
            }

            if (expert.Photo.IsOlder216Kb())
            {
                ModelState.AddModelError("Photo", "Photo Max 256Kb");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "img");
            expert.Image = await expert.Photo.SaveFileAsync(folder);
            #endregion

            expert.PositionId=positionId;
            await _db.Experts.AddAsync(expert);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            if (id == null)
                return NotFound();
            Expert dbexpert = await _db.Experts.FirstOrDefaultAsync(x=>x.Id == id);
            if (dbexpert == null)
                return BadRequest();

            return View(dbexpert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id , Expert expert,int positionId)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            if (id == null)
                return NotFound();
            Expert dbexpert = await _db.Experts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbexpert == null)
                return BadRequest();

            #region Photo
            if(expert.Photo != null)
            {
                if (!expert.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Just select image type");
                    return View();
                }

                if (expert.Photo.IsOlder216Kb())
                {
                    ModelState.AddModelError("Photo", "Max 256Kb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                expert.Image = await expert.Photo.SaveFileAsync(folder);
                string path =Path.Combine(_env.WebRootPath,folder, dbexpert.Image);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbexpert.Image = expert.Image;
            }
            #endregion
            dbexpert.PositionId = positionId;
            dbexpert.FullName = expert.FullName;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Expert dbexpert = await _db.Experts.Include(x=>x.Position).FirstOrDefaultAsync(x => x.Id==id);
            if (dbexpert == null)
                return BadRequest();

            return View(dbexpert);
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Expert dbexpert = await _db.Experts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbexpert == null)
                return BadRequest();

            if (dbexpert.IsDeactive)
                dbexpert.IsDeactive = false;
            else
                dbexpert.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
