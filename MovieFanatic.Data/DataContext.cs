using System.Data.Entity;
using MovieFanatic.Domain;

namespace MovieFanatic.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("MovieFanatic") { }

        public DbSet<Movie> Movies { get; set; }
    }
}