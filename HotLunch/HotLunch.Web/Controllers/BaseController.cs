using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotLunch.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ILog Logger { get; private set; }

        public BaseController()
        {
            this.Logger = LogManager.GetLogger(this.GetType().Name);
        }
    }
}