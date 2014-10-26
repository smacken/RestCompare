using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.WebApi;
using Repository;
using Repository.EntityFramework;
using RestCompare.Data;
using RestCompare.cli.Controllers;

namespace RestCompare.cli
{
    public class ApiContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof (ProductController).Assembly);
            builder.RegisterGeneric(typeof (EFRepository<Db, KeyedObject<int>,int>))
                   .As(typeof (EFRepository<Db, KeyedObject<int>, int>));
            base.Load(builder);
        }
    }
}
