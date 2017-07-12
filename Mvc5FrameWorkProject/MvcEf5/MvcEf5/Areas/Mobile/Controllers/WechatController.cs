using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Areas.Mobile.Controllers
{
    public class WechatController : Controller
    {
        //
        // GET: /Mobile/Wechat/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GotoPc()
        {
            var urldefault = "~/pc/Windowspc/index";
            //var urldefault = Url.Action("index", "windowspc", new { area = "" });
            return Redirect(urldefault);
        }
    }
}