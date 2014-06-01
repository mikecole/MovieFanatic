using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class CharacterConfiguration : EntityTypeConfiguration<Character>
    {
        public CharacterConfiguration()
        {
            Property(charac => charac.Name).HasMaxLength(400).IsRequired();

            HasRequired(charac => charac.Movie)
                .WithMany(movie => movie.Characters);

            HasRequired(charac => charac.Actor)
                .WithMany(actor => actor.Characters);
        }
    }
}