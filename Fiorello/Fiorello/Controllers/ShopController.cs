using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _db;

        public ShopController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductsCount = await _db.Products.Where(x=>!x.IsDeactive).CountAsync();

            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            Product product = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            
            if (id == null)
                return NotFound();
            Product dbproduct = await _db.Products.Include(x=>x.ProductDetail).FirstOrDefaultAsync(x=>x.Id == id);
            if (dbproduct == null)
                return BadRequest();

            return View(dbproduct);     
        }

        public async Task<IActionResult> LoadMore(int skipCount)
        {
			List<Product> product = await _db.Products.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).Skip(skipCount).Take(8).ToListAsync();
            return PartialView("_LoadMoreProductsPartial",product);
        }
    }
}
