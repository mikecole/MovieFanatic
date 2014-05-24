using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new ProductionCompanyConfiguration());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors))
                {
                    ErrorSignal.FromCurrentContext().Raise(new Exception(String.Format("Validation Error :: {0} - {1}", item.PropertyName, item.ErrorMessage)));
                }

                throw;
            }
        }
    }
}