using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Areas.PC.Controllers
{
    public class WindowspcController : Controller
    {
        //
        // GET: /PC/Windowspc/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GotoMasterDeault()
        {
            return RedirectToAction("Index", "Home", new { });
        }
	}
}