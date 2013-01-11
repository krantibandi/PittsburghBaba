using System.Linq;
using System.Web.Mvc;

namespace PittsburghBabaTemple.Core.Web
{
    public class PbtViewEngine : RazorViewEngine
    {
        public PbtViewEngine()
        {
            ViewLocationFormats = ViewLocationFormats.Union(new[] { "~/Views/Admin/{1}/{0}.cshtml" }).ToArray();
        }
    }
}
