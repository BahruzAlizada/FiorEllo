using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _db;

        public SliderInfoController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            SliderInfo sliderInfo = await _db.SliderInfo.FirstOrDefaultAsync();

            return View(sliderInfo);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            SliderInfo dbsliderinfo = await _db.SliderInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbsliderinfo == null)
                return BadRequest();

            return View(dbsliderinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,SliderInfo sliderInfo)
        {
            if (id == null)
                return NotFound();
            SliderInfo dbsliderinfo = await _db.SliderInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (dbsliderinfo == null)
                return BadRequest();

            dbsliderinfo.Title = sliderInfo.Title;
            dbsliderinfo.SubTitle = sliderInfo.SubTitle;
            dbsliderinfo.Description= sliderInfo.Description;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            SliderInfo dbsliderInfo = await _db.SliderInfo.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbsliderInfo == null)
                return BadRequest();

            return View(dbsliderInfo);
        }
    }
}
