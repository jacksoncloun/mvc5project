using System.Web;
using System.Web.Mvc;

namespace MvcEf5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //注册自定义全局错误
            //filters.Add(new HandleErrorAttribute() { ExceptionType = typeof(System.Data.DataException), View = "Error5002.cshtml", Order = 1 });//这里的View的视图文件应该放到当前请求的文件夹或共享视图文件里
            //filters.Add(new HandleErrorAttribute());//初始化网站全局错误处理器
        }
    }
}
