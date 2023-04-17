using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionsController : Controller
    {
        private readonly AppDbContext _db;

        public PositionsController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Position position)
        {
            bool isExist = await _db.Positions.AnyAsync(x=>x.PositionName==position.PositionName);
            if (isExist)
            {
                ModelState.AddModelError("PositionName", "This Position already is Exist!");
                return View();
            }

            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbposition == null)
                return BadRequest();


            return View(dbposition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Position position)
        {
            if (id == null)
                return NotFound();
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbposition == null)
                return BadRequest();

            bool isExist = await _db.Positions.AnyAsync(x => x.PositionName == position.PositionName && x.Id != position.Id);
            if (isExist)
            {
                ModelState.AddModelError("PositionName", "This Position already is Exist!");
                return View();
            }

            dbposition.PositionName = position.PositionName;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbposition == null)
                return BadRequest();
           

            return View(dbposition);
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if(id == null)
                return NotFound();
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbposition == null)
                return BadRequest();

            if(dbposition.IsDeactive)
                dbposition.IsDeactive = false;
            else
                dbposition.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
