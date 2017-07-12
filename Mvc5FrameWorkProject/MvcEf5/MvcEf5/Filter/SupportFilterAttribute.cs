using Services.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcEf5.Filter
{
    /// <summary>
    ///　权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SupportFilterAttribute : ActionFilterAttribute
    {
        filterContextInfo fcinfo;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var issessionClear = WebSession.GetSetLoginSession();

            fcinfo = new filterContextInfo(filterContext);
            //fcinfo.actionName;//获取域名
            //fcinfo.controllerName;获取 controllerName 名称

            bool isstate = issessionClear.issuccess;
            //islogin = false;
            if (isstate)//如果满足
            {
                //逻辑代码
                // filterContext.Result = new HttpUnauthorizedResult();//直接URL输入的页面地址跳转到登陆页  
                // filterContext.Result = new RedirectResult("http://www.baidu.com");//也可以跳到别的站点
                //filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "product", action = "Default" }));
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Index", false);
                //filterContext.Result = new RedirectToRouteResult(new { controller = "", actionre = "", area = "" });
                //filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };// 直接返回 return Content("抱歉,你不具有当前操作的权限！")
            }
        }
    }


    public class filterContextInfo
    {
        public filterContextInfo(ActionExecutingContext filterContext)
        {
            #region 获取链接中的字符
            // 获取域名
            domainName = filterContext.HttpContext.Request.Url.Authority;

            //获取模块名称
            //  module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();

            //获取 controllerName 名称
            controllerName = filterContext.RouteData.Values["controller"].ToString();

            //获取ACTION 名称
            actionName = filterContext.RouteData.Values["action"].ToString();

            #endregion
        }
        /// <summary>
        /// 获取域名
        /// </summary>
        public string domainName { get; set; }
        /// <summary>
        /// 获取模块名称
        /// </summary>
        public string module { get; set; }
        /// <summary>
        /// 获取 controllerName 名称
        /// </summary>
        public string controllerName { get; set; }
        /// <summary>
        /// 获取ACTION 名称
        /// </summary>
        public string actionName { get; set; }

    }
}