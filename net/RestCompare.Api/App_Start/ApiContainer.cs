using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using RestCompare.Api.Controllers;
using SharpRepository.EfRepository;
using SharpRepository.Repository;

namespace RestCompare.Api.App_Start
{
    public class ApiContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Db>().As<DbContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EfRepository<,>)).As(typeof(IRepository<,>));
            builder.RegisterApiControllers(typeof(ProductsController).Assembly);

            base.Load(builder);
        }
    }
}