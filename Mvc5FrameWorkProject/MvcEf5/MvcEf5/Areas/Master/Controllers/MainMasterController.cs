using MvcEf5.Controllers;
using MvcEf5.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Areas.Master.Controllers
{
    [SupportFilterAttribute]
    public class MainMasterController : BaseController
    {
        //
        // GET: /Master/MainMaster/
        public ActionResult Index()
        {
            return View();
        }
    }
}