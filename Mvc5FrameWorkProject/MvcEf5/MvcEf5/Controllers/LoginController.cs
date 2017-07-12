using Models;
using Services.Bll;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace MvcEf5.Controllers
{
    public class LoginController : BaseController
    {
        ILoginServices loginServices;
        public LoginController()
        {
            loginServices = new LoginServices();
        }
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(FormCollection entities)
        {
            Users us = new Users();
            us.username = entities["LoginName"];
            us.userpwd = entities["LoginPassword"];
            var loginresult = loginServices.Loginres(us);
            return Json(new { Success = loginresult.issuccess, Message = loginresult.decription });
        }
	}
}