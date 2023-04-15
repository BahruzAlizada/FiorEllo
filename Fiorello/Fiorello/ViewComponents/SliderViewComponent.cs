using Fiorello.DAL;
using Fiorello.VIewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public SliderViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = await _db.Sliders.ToListAsync(),
                SliderInfo = await _db.SliderInfo.FirstOrDefaultAsync()
            };

            return View(homeVM);
        }
    }
}
