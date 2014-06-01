using System.Data.Entity.ModelConfiguration;
using MovieFanatic.Domain;

namespace MovieFanatic.Data.Configurations
{
    public class ProductionCompanyConfiguration : EntityTypeConfiguration<ProductionCompany>
    {
        public ProductionCompanyConfiguration()
        {
            Property(comp => comp.Name).HasMaxLength(100).IsRequired();
        }
    }
}