using Microsoft.EntityFrameworkCore;
using TestXisMitraUtama.Model;

namespace TestXisMitraUtama.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(e =>
            {
                e.HasKey(e => e.Id);

                e.ToTable("movies");
                
                e.Property(e => e.Title)
                .IsRequired();
                e.Property(e => e.Rating)
                 .HasDefaultValue(0);
            });
        }
    }
}
