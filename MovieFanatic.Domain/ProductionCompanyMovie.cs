using System.Collections.Generic;

namespace MovieFanatic.Domain
{
    public class ProductionCompanyMovie : EntityBase
    {
        public ProductionCompanyMovie(ProductionCompany productionCompany, Movie movie)
            : this()
        {
            ProductionCompany = productionCompany;
            Movie = movie;
        }
        public ProductionCompanyMovie() { }

        public ProductionCompany ProductionCompany { get; private set; }
        public Movie Movie { get; private set; }
    }
}