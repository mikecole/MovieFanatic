using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class MovieGenreConfiguration : EntityTypeConfiguration<MovieGenre>
    {
        public MovieGenreConfiguration()
        {
            HasRequired(mGenre => mGenre.Genre)
                .WithMany(genre => genre.MovieGenres);

            HasRequired(mGenre => mGenre.Movie)
                .WithMany(movie => movie.MovieGenres);
        }
    }
}