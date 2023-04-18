using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Product dbproduct = await _db.Products.Include(x=>x.ProductDetail).FirstOrDefaultAsync(x=>x.Id == id);
            if (dbproduct == null)
                return BadRequest();

            return View(dbproduct);
                 
        }
    }
}
