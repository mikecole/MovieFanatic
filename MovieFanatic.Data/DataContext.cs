using System.Data.Entity;
using MovieFanatic.Data.Configurations;
using MovieFanatic.Domain;

namespace MovieFanatic.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("MovieFanatic") { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
        }
    }
}