using MovieFanatic.Data;
using MovieFanatic.Domain;
using MovieFanatic.Web.Infrastructure.Authentication;
using StructureMap.Configuration.DSL;

namespace MovieFanatic.Web.Infrastructure.IoC
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<DataContext>().HttpContextScoped().Use<DataContext>(); //Remember to dispose of this in global.asax!
            For<IAuthenticator>().Use<Authenticator>();
        }
    }
}