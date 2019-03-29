using Microsoft.EntityFrameworkCore;
namespace Pokemon.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Core.Entities.Pokemon> Pokemon { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
