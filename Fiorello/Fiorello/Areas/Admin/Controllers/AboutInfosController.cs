using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutInfosController : Controller
    {
        private readonly AppDbContext _db;

        public AboutInfosController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<AboutInfo> aboutInfos = await _db.AboutInfo.ToListAsync();
            return View(aboutInfos);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AboutInfo aboutInfo)
        {
            bool isExist=await _db.AboutInfo.AnyAsync(x=>x.Name==aboutInfo.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This title already is Exist!");
                return View();
            }

            await _db.AboutInfo.AddAsync(aboutInfo);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            AboutInfo dbaboutinfo = await _db.AboutInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbaboutinfo == null)
                return BadRequest();

            return View(dbaboutinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,AboutInfo aboutInfo)
        {
            if (id == null)
                return NotFound();
            AboutInfo dbaboutinfo = await _db.AboutInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbaboutinfo == null)
                return BadRequest();

            bool isExist = await _db.AboutInfo.AnyAsync(x=>x.Name==aboutInfo.Name && x.Id!=aboutInfo.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This title already is Exist!");
                return View();
            }

            dbaboutinfo.Name = aboutInfo.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            AboutInfo dbaboutinfo = await _db.AboutInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbaboutinfo == null)
                return BadRequest();

            return View(dbaboutinfo);
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            AboutInfo dbaboutinfo = await _db.AboutInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbaboutinfo == null)
                return BadRequest();

            if(dbaboutinfo.IsDeactive)
                dbaboutinfo.IsDeactive= false;
            else
                dbaboutinfo.IsDeactive=true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
