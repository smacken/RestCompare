using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace RestCompare.cli
{
    public class ApiContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new Car(c.Resolve<IDriver>())).As<IVehicle>();
            base.Load(builder);
        }
    }
}
