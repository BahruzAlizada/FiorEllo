using Fiorello.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfo { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<AboutInfo> AboutInfo { get; set; }
    }
}
