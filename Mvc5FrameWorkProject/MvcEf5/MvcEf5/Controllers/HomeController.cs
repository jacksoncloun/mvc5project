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
    //[SupportFilterAttribute]
    public class HomeController : BaseController
    {
        IUserServices _userServices;
        IRedisService _redisService;
        public HomeController()
        {
            _userServices = new UserServices();
            _redisService = new RedisService();
            //_redisService.SaveInRedis();
        }

        public ActionResult Index()
        {
            _redisService.SaveInRedis();
            RedisValue lister = RedisHelper.GetStringKey("Users");
            List<Users> model = _userServices.getAlluser();
            return View(model);
        }
        public ActionResult Clear()
        {
            WebSession.SessionClear();
            return RedirectToAction("Index","Home");
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