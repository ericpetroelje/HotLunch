using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HotLunch.Web.Util
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString BootstrapValidationClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);
            string modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);

            if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName))
            {
                return null;
            }

            ModelState modelState = htmlHelper.ViewData.ModelState[modelName];
            ModelErrorCollection modelErrors = (modelState == null) ? null : modelState.Errors;
            ModelError modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

            if (modelError == null)
            {
                return null;
            }
            else
            {
                return MvcHtmlString.Create("has-error");
            }
        }

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper helper)
        {
            if (helper.ViewData.ModelState.IsValid)
                return MvcHtmlString.Empty;

            StringBuilder sbHtml = new StringBuilder();
            foreach (var key in helper.ViewData.ModelState.Keys.Where(k => string.IsNullOrWhiteSpace(k)))
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    sbHtml.AppendFormat("<p class='bg-danger' style='padding: 5px'>{0}</p>",helper.Encode(err.ErrorMessage));
            }

            return MvcHtmlString.Create(sbHtml.ToString());
        }
    }
}