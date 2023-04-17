using Fiorello.DAL;
using Fiorello.VIewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class ExpertViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public ExpertViewComponent(AppDbContext db)
        {
            _db = db; 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeVM homeVM = new HomeVM
            {
                Experts = await _db.Experts.Where(x=>!x.IsDeactive).OrderByDescending(x=>x.Id).Take(4).ToListAsync(),
                Positions = await _db.Positions.Where(x=>!x.IsDeactive).ToListAsync(),
            };
            return View(homeVM);
        }
    }
}
