using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Controllers
{
    //[HandleError(ExceptionType = typeof(System.Data.DataException), View = "Error5002.cshtml")]
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HandleError(ExceptionType=typeof(System.Data.DataException),View="Error5002.cshtml")]
        public ActionResult ErrorIndex2()
        {
            return View("Index2");
        }
	}
}