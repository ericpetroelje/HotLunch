using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotLunch.Web.DependencyInjection
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private IContainer StructureMapContainer { get; set; }

        public StructureMapControllerFactory(IContainer container)
        {
            StructureMapContainer = container;
        }

        #region Overridden Members

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //http://stackoverflow.com/questions/923926/structuremap-controller-factory-and-null-controller-instance-in-mvc
            //see last answer - makes this return a 404, which is the default asp.net behaviour - instead of our previous
            //custom code to do the same thing, which resulted in wanton error logs
            if (controllerType == null)
                return base.GetControllerInstance(requestContext, controllerType);

            return StructureMapContainer.GetInstance(controllerType) as Controller;
        }

        #endregion
    }
}