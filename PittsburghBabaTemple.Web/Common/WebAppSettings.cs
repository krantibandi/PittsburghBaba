using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PittsburghBabaTemple.Web.Common
{
    public static class WebAppSettings
    {
        #region IAppSettings Members

        public static string WebUrl { get { return ConfigurationManager.AppSettings["webURL"]; } }
        public static string HelpUrl { get { return ServerName + HelpPath; } }
        public static string UserActivationUrl { get { return ConfigurationManager.AppSettings["UserActivationUrl"]; ; } }
        public static string HelpPath { get { return ConfigurationManager.AppSettings["HelpPath"]; } }
        public static string PromptAfter { get { return ConfigurationManager.AppSettings["PromptAfter"]; } }
        public static string ShowDialogFor { get { return ConfigurationManager.AppSettings["ShowDialogFor"]; } }
        public static bool ShowAnnouncementsFromPreviousYear { get { return ConfigurationManager.AppSettings["ShowAnnouncementsFromPreviousYear"] == "True"; } }
        public static int NumberOfAnnouncementsToShow { get { return Int32.Parse(ConfigurationManager.AppSettings["AnnouncementsToShow"]); } }
        public static string Environment { get { return ConfigurationManager.AppSettings["Environment"]; } }

        public static string ServerName
        {
            get
            {
                var port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                if (port == null || port == "80" || port == "443")
                    port = string.Empty;
                else
                    port = ":" + port;

                var protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (protocol == null || protocol == "0")
                    protocol = "http://";
                else
                    protocol = "https://";

                var servername = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port;

                return servername;
            }
        }

        #endregion
    }
}