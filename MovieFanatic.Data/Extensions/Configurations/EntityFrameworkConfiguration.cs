using System.Data.Entity;
using MovieFanatic.Data.Extensions.Interceptors;

namespace MovieFanatic.Data.Configurations
{
    public class EntityFrameworkConfiguration : DbConfiguration
    {
        public EntityFrameworkConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
        }
    }
}