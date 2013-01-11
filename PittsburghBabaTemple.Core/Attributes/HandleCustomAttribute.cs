using System;
using System.Web;
using System.Web.Mvc;
using PittsburghBabaTemple.Core.Model;
using log4net;

namespace PittsburghBabaTemple.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class HandleCustomError : ActionFilterAttribute, IExceptionFilter
    {

        public HandleCustomError()
        {
            // Errors come here before other error filters
            Order = 1;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var exception = filterContext.Exception;
            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, exception).GetHttpCode() != 500)
            {
                return;
            }

            var controller = filterContext.Controller as Controller;
            if (controller == null || filterContext.ExceptionHandled)
                return;


            if (exception == null)
                return;

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            var handleErrorInfo = new HandleErrorInfo(exception, (string)controllerName, (string)actionName);

            var model = new ViewDataDictionary<HandleErrorInfo>(handleErrorInfo);

            try
            {
                var errorLog = new ErrorLog()
                {
                    Action = actionName,
                    Controller = controllerName,
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionSource = filterContext.Exception.Source,
                    ExceptionTargetSite = filterContext.Exception.TargetSite.DeclaringType.Name + '.' +
                    filterContext.Exception.TargetSite.Name,
                    ExceptionType = filterContext.Exception.GetType().Name,
                    StackTrace = filterContext.Exception.StackTrace
                };
                errorLog.Save();
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType()).Fatal("Unable to Log Exception : ", e);
                LogManager.GetLogger(GetType()).Fatal("Original Exception Was : ", filterContext.Exception);
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new PartialViewResult()
                {
                    ViewData = model,
                    ViewName = "CustomError"
                };
            }
            else
            {
                filterContext.Result = new ViewResult()
                {
                    ViewData = model,
                    ViewName = "CustomError"
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}
