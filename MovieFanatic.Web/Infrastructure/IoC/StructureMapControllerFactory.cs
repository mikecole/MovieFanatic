using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace MovieFanatic.Web.Infrastructure.IoC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                requestContext.HttpContext.Response.StatusCode = 404;
                throw new HttpException(404, "Page not found.");
            }

            var controller = ObjectFactory.GetInstance(controllerType);

            return (IController)controller;
        }
    }
}