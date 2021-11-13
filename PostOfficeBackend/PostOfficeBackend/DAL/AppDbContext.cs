using Microsoft.EntityFrameworkCore;
using PostOfficeBackend.DAL.Entities;

namespace PostOfficeBackend.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Parcel> Parcels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                .HasMany(c => c.Parcels)
                .WithOne(e => e.Post);
        }
    }
}
