using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotLunch.Web.Util
{
    #region ModelStateTempDataTransfer
    // Model State Transfer attributes from here:
    // http://weblogs.asp.net/rashid/archive/2009/04/01/asp-net-mvc-best-practices-part-1.aspx
    public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;

        protected void StoreModelStateInTempData(ActionExecutedContext filterContext, bool encryptIt)
        {
            filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
        }

        protected void MergeModelStateFromTempData(ActionExecutedContext filterContext)
        {
            ModelStateDictionary modelState = filterContext.Controller.TempData[Key] as ModelStateDictionary;

            if (modelState != null)
            {
                filterContext.Controller.ViewData.ModelState.Merge(modelState);
            }
        }
    }
    #endregion

    #region ExportModelStateToTempData
    public class ExportModelStateToTempData : ModelStateTempDataTransfer
    {
        private bool _OnlyExportIfInvalid;
        private bool _EncryptModelState;

        public ExportModelStateToTempData(bool OnlyExportIfInvalid = true, bool EncryptModelState = false) : base()
        {
            _OnlyExportIfInvalid = OnlyExportIfInvalid;
            _EncryptModelState = EncryptModelState;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Only export when ModelState is not valid
            if (!filterContext.Controller.ViewData.ModelState.IsValid || !_OnlyExportIfInvalid)
            {
                //Export if we are redirecting
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                {
                    StoreModelStateInTempData(filterContext, _EncryptModelState);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
    #endregion

    #region ImportModelStateFromTempData
    public class ImportModelStateFromTempData : ModelStateTempDataTransfer
    {
        public ImportModelStateFromTempData() : base()
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Only Import if we are viewing
            if (filterContext.Result is ViewResult)
            {
                MergeModelStateFromTempData(filterContext);
            }
            else
            {
                //Otherwise remove it.
                filterContext.Controller.TempData.Remove(Key);
            }

            base.OnActionExecuted(filterContext);
        }
    }
    #endregion
}