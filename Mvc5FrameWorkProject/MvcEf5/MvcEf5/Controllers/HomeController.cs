using Enums;
using Models;
using MvcEf5.Filter;
using Newtonsoft.Json;
using Response;
using Response.Redis;
using Services.Bll;
using Services.IBll;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEf5.Controllers
{
    //[SupportFilterAttribute]   //验证权限
    public class HomeController : BaseController
    {
        IUserServices _userServices;
        IRedisService _redisService;
        public HomeController()
        {
            _userServices = new UserServices();
            _redisService = new RedisService();
            _redisService.SaveInRedis();
        }

        public ActionResult Index()
        {
            List<Users> model = RedisHelper.HashGetAll<Users>("Users");
            return View(model);
        }
        public ActionResult Clear()
        {
            WebSession.SessionClear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(string N, int id)
        {
            EntitiesNames en = new EntitiesNames();
            N = getClassNames<EntitiesNames>(N, en);
            if (N != "")
            {
                var model = RedisHelper.GetHashKey<Users>(N, N + "-Id-" + id.ToString());
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection entity)
        {
            //string[] keys = entity.AllKeys;
            //Users u = new Users();
            //u.id = (int)keys["id"];


            return Json(new { Success = true });
        }
        public ActionResult Del(string N, int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Del(FormCollection entity)
        {
            return Json(new { Success = true });
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}