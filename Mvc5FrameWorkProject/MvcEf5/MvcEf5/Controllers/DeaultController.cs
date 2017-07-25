using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Controllers
{
    public class DeaultController : Controller
    {
        public readonly A _a;
        public readonly string c1;
        public DeaultController()
        {
            this._a = new A();
            c1 = "1";
        }

        //
        // GET: /Deault/
        public JsonResult Index()
        {
            _a.name = "bbb";
            //c1 = "ee";
            return Json(_a, JsonRequestBehavior.AllowGet);
        }
    }
    public class A
    {
        public A()
        {
            name = "aaa";
        }
        public int id { get; set; }
        public string name { get; set; }
    }
}