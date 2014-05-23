using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            Property(user => user.Title).HasMaxLength(100).IsRequired();
        }
    }
}