using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation.WebApi;
using RestCompare.Api.App_Start;

namespace RestCompare.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiContainer());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }

}
