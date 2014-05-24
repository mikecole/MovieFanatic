using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(user => user.Name).HasMaxLength(50).IsRequired();
        }
    }
}