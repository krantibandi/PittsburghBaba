using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PittsburghBabaTemple.Core.Attributes;
using PittsburghBabaTemple.Core.Data;
using PittsburghBabaTemple.Core.Extensions;
using PittsburghBabaTemple.Core.Web;
using NHibernate;
using NHibernate.Context;
using PittsburghBabaTemple.Web.Common;
using PittsburghBabaTemple.Web.Attributes;

namespace PittsburghBabaTemple.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    //public class MvcApplication : System.Web.HttpApplication
    //{
    //    protected void Application_Start()
    //    {
    //        AreaRegistration.RegisterAllAreas();

    //        WebApiConfig.Register(GlobalConfiguration.Configuration);
    //        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    //        RouteConfig.RegisterRoutes(RouteTable.Routes);
    //        BundleConfig.RegisterBundles(BundleTable.Bundles);
    //        AuthConfig.RegisterAuth();
    //    }
    //}

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ISessionFactory _sessionFactory;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleCustomError());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //routes.MapRouteLowercase(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Login", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PbtViewEngine());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DataTypeAttribute), typeof(DataTypeAttributeAdapter));
            AutoMapperConfiguration.Configure();
            _sessionFactory = SessionConfiguration.Setup();
            //#if DEBUG
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            //#endif

            #region enable the following to enable route debugging.  No other step is required.
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            #endregion

        }

        protected void Application_BeginRequest()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest()
        {
            if (_sessionFactory != null)
            {
                try
                {
                    var session = CurrentSessionContext.Unbind(_sessionFactory);
                    if (session != null) { session.Dispose(); }
                }
                finally
                {
                    _sessionFactory.Dispose();
                }
            }

        }

        protected void Application_End()
        {
            if (_sessionFactory != null)
            {
                try
                {
                    ISession session = CurrentSessionContext.Unbind(_sessionFactory);
                    if (session != null) { session.Dispose(); }
                }
                finally
                {
                    _sessionFactory.Dispose();
                }
            }
        }
    }
}