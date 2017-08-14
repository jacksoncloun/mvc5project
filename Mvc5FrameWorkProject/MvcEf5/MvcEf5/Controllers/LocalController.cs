using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Controllers
{
    public class LocalController : Controller
    {
        //
        // GET: /Local/
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 回调方法
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult GetRequest()
        {
            var json = HttpContext.Request.QueryString["data"];
            string filePath = Server.MapPath("~/msg.txt");
            //D:\WebSites\Delivery\Temp
            if (!System.IO.File.Exists(filePath))
            {
                FileStream f = System.IO.File.Create(filePath);
                f.Close();
            }
            StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.GetEncoding("utf-8"));
            lock (sw)
            {
                var str = JsonConvert.SerializeObject(json);//JsonHelper.EntityToJson(json);//
                sw.Write(DateTime.Now.ToString("yyyyMMddHHmmssfff") + str + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\r\n");
                //sw.Write(json + "666666666666666"+json2+json3+json1);
                sw.Close();
            }
            return Content(JsonConvert.SerializeObject(json));
        }

        public ActionResult pushGetRequest()
        {
            return Content("");
        }
	}
}