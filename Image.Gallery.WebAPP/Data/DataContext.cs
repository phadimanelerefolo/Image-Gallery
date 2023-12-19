using Microsoft.EntityFrameworkCore;

namespace Image.Gallery.WebAPP.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Models.Image> Images { get; set; }
    }
}
