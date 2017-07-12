using Models;
using Services.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Response.Redis;

namespace MvcEf5.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var returns = WebSession.GetSetLoginSession() ?? new LoginResult();
            ViewBag.LoginMessage = returns;
        }
    }
}