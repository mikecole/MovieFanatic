using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MovieFanatic.Domain
{
    public class ProductionCompany : EntityBase
    {
        public ProductionCompany(string name)
            : this()
        {
            Name = name;
        }
        private ProductionCompany()
        {
            ProductionCompanyMovies = new Collection<ProductionCompanyMovie>();
        }

        public string Name { get; private set; }

        public ICollection<ProductionCompanyMovie> ProductionCompanyMovies { get; private set; }
    }
}