using Fiorello.DAL;
using Fiorello.VIewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public AboutViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeVM homeVM = new HomeVM
            {
                About = await _db.About.FirstOrDefaultAsync(),
                AboutInfo = await _db.AboutInfo.Take(3).Where(x=>!x.IsDeactive).ToListAsync()
            };

            return View(homeVM);
        }
    }
}
