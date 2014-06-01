using System.Web.Mvc;
using StructureMap;

namespace MovieFanatic.Web.Infrastructure.IoC
{
    public static class StructureMapConfiguration
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(cfg => cfg.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.LookForRegistries();
                scanner.WithDefaultConventions();
            }));

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}