using HotLunch.Web.DependencyInjection;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HotLunch.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("~/log4net.config")));
            StructureMapBootstrapper.Configure(IoC.Container);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            SetLog4NetPropertiesForRequest();
        }

        private void SetLog4NetPropertiesForRequest()
        {
            // Set properties used by log4net
            log4net.ThreadContext.Properties.Remove("Url");
            if (HttpContext.Current?.Request?.Url != null)
            {
                log4net.ThreadContext.Properties["Url"] = HttpContext.Current.Request.Url.ToString();
            }


            log4net.ThreadContext.Properties.Remove("HttpUserAgent");
            if (HttpContext.Current?.Request?.UserAgent != null)
            {
                log4net.ThreadContext.Properties["HttpUserAgent"] = HttpContext.Current.Request.UserAgent;
            }

            log4net.ThreadContext.Properties.Remove("HttpReferrer");
            if (HttpContext.Current?.Request?.UrlReferrer != null)
            {
                log4net.ThreadContext.Properties["HttpReferrer"] = HttpContext.Current.Request.UrlReferrer.ToString();
            }
        }
    }
}
