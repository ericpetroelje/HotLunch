using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;

namespace HotLunch.Web.DependencyInjection
{
    public static class IoC
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = Initialize();
                }

                return container;
            }
        }

        private static IContainer Initialize()
        {
            return new Container();
        }
    }
}