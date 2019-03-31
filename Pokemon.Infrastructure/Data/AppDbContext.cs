using Microsoft.EntityFrameworkCore;
namespace Pokemon.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public  DbSet<Core.Entities.Pokemon> Pokemon { get; set; }
        public  DbSet<Core.Entities.Evolution> Evolution { get; set; }
        public  DbSet<Core.Entities.Move> Move { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
