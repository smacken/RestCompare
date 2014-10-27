using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation.WebApi;
using Owin;

namespace RestCompare.cli
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            //config.Routes.MapHttpRoute("defaultVersioned", "v{version}/{controller}/{id}", new { id = RouteParameter.Optional }, new { version = @"\d+" });
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiContainer());
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            FluentValidationModelValidatorProvider.Configure(config);

            appBuilder.UseWebApi(config);
        }
    }

    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory>
        GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>
            (inherit: true);
        }
    }
}
