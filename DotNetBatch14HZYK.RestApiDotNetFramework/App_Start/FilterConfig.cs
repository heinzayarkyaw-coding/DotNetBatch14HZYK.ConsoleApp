using System.Web;
using System.Web.Mvc;

namespace DotNetBatch14HZYK.RestApiDotNetFramework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
