using HotLunch.Domain.Library.Schools;
using HotLunch.Domain.Repository.Schools;
using HotLunch.Domain.Services.Schools;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotLunch.Web.DependencyInjection
{
    public class StructureMapBootstrapper
    {
        public static void Configure(IContainer container)
        {
            // This sets it up so that StructureMap creates controllers and can inject stuff into them.
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(container));

            // TODO: Configure stuff here.
            container.Configure(ConfigureRepositories);
            container.Configure(ConfigureServices);
        }

        private static void ConfigureRepositories(ConfigurationExpression x)
        {
            x.For<ISchoolRepository>().Use<SchoolRepository>().Ctor<string>().Is("HotLunch").Singleton();
        }

        private static void ConfigureServices(ConfigurationExpression x)
        {
            x.For<ISchoolService>().Use<SchoolService>().Singleton();
        }
    }
}