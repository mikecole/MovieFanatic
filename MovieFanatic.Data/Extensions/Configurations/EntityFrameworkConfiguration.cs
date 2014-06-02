using System.Data.Entity;
using EntityFramework.Filters;
using MovieFanatic.Data.Extensions.Interceptors;

namespace MovieFanatic.Data.Extensions.Configurations
{
    public class EntityFrameworkConfiguration : DbConfiguration
    {
        public EntityFrameworkConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
            AddInterceptor(new FilterInterceptor());
        }
    }
}