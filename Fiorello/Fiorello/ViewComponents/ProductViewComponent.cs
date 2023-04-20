using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.VIewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
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

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Product> product = new List<Product>();

            if(take==0)
                product = await _db.Products.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            else
                product = await _db.Products.Where(x => !x.IsDeactive).OrderByDescending(x=>x.Id).Take(take).ToListAsync();

            return View(product);
        }
    }
}
