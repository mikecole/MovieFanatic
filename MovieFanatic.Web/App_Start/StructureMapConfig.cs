using MovieFanatic.Web.App_Start;
using MovieFanatic.Web.Infrastructure.IoC;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(StructureMapConfig), "RegisterStructureMap")]
namespace MovieFanatic.Web.App_Start
{
    public static class StructureMapConfig
    {
        public static void RegisterStructureMap()
        {
            StructureMapConfiguration.Initialize();
        }
    }
}