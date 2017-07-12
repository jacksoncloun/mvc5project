using Services.Bll;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Controllers
{
    public class DemoController : Controller
    {
        IUserServices _userServices;

        public DemoController()
        {
            _userServices = new UserServices();
        }

        //
        // GET: /Demo/
        public ActionResult Index()
        {
            var model = _userServices.getAlluser();
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection entities)
        {

            return View();
        }

        public JsonResult Index2(string d)
        {
            return Json(new { success = "成功", message = d }, JsonRequestBehavior.AllowGet);
        }
	}
}