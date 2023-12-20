using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Image.Gallery.WebAPP.Data
{
    public class DataContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Models.Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!String.IsNullOrEmpty(_configuration.GetConnectionString("postgres")))
                {
                    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("postgres"), x => x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo"));
                }
                else
                    base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
