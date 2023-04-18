using Fiorello.DAL;
using Fiorello.VIewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public ProductViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeVM homeVM = new HomeVM
            {
                Products = await _db.Products.Where(x=>!x.IsDeactive).ToListAsync(),
                Categories = await _db.Categories.Where(x=>!x.IsDeactive).ToListAsync()
            };

            return View(homeVM);
        }
    }
}
