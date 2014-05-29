using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.InteropServices;
using Elmah;
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
        public DbSet<ProductionCompany> ProductionCompanies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<ProductionCompanyMovie> ProductionCompanyMovies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new ProductionCompanyConfiguration());
            modelBuilder.Configurations.Add(new CharacterConfiguration());
            modelBuilder.Configurations.Add(new ActorConfiguration());
            modelBuilder.Configurations.Add(new MovieGenreConfiguration());
            modelBuilder.Configurations.Add(new ProductionCompanyMovieConfiguration());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entity in ex.EntityValidationErrors)
                {
                    foreach (var item in entity.ValidationErrors)
                    {
                        ErrorSignal.FromCurrentContext().Raise(new Exception(String.Format("Validation Error :: {0}.{1} - {2}. Attempted to save {3}.", entity.Entry.Entity, item.PropertyName, item.ErrorMessage, entity.Entry.CurrentValues)));
                    }
                }

                throw;
            }
        }
    }
}