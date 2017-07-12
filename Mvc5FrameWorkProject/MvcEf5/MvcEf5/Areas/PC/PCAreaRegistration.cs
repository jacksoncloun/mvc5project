using System.Web.Mvc;

namespace MvcEf5.Areas.PC
{
    public class PCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "PC_default",
                url: "PC/{controller}/{action}/{id}",
               defaults: new { controller = "Windowspc", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}