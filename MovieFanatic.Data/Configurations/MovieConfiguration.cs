using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            Property(movie => movie.Title).HasMaxLength(100).IsRequired();
            Property(movie => movie.AverageRating).HasPrecision(4, 2);
        }
    }
}