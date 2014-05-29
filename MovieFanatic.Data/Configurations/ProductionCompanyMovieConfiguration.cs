using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class ProductionCompanyMovieConfiguration : EntityTypeConfiguration<ProductionCompanyMovie>
    {
        public ProductionCompanyMovieConfiguration()
        {
            HasRequired(pcm => pcm.Movie)
                .WithMany(movie => movie.ProductionCompanyMovies);

            HasRequired(pcm => pcm.ProductionCompany)
                .WithMany(comp => comp.ProductionCompanyMovies);
        }
    }
}