using System.Web;
using System.Web.Mvc;

namespace buildschool_chu
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new EnableCorsAttribute());
        }
    }

    public class EnableCorsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            response.AddHeader("Access-Control-Allow-Headers", "Authorization, Content-Type");
            response.AddHeader("Access-Control-Allow-Methods", "POST, PUT, GET, DELETE, OPTIONS");
            response.AddHeader("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(filterContext);
        }
    }


}
