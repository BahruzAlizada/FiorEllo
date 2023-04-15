using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public BlogViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Blog> blogs = new List<Blog>();

            if (take == 3)
            {
                blogs = await _db.Blogs.OrderByDescending(x=>x.Id).Take(take).ToListAsync();
            }
            else
            {
                blogs = await _db.Blogs.OrderByDescending(x => x.Id).ToListAsync();

            }

            return View(blogs);
        }
    }
}
