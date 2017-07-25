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

        /// <summary>
        /// 第一种错误处理机制，控制器继承自BaseController
        /// 如果页面出错跳转到错误页面  BaseController 中  自动使用basecontroller中的错误处理逻辑 
        /// 因为继承了 BaseController 控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorIndex()
        {
            //List<Users> model = RedisHelper.HashGetAll<Users>("Users");
            //return View("Index");
            throw new Exception("This is Test Error");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorIndex2()
        {
            var a = "11";
            var b = "ddddd";
            var c = int.Parse(a) + int.Parse(b);
            return View("Index");
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

            Users u = _userServices.GetUserById(int.Parse(entity.GetValue("id").AttemptedValue)); //new Users();
            //u.id = int.Parse(entity.GetValue("id").AttemptedValue);
            u.username = entity.GetValue("username").AttemptedValue;
            u.isdelete = entity.GetValue("isdelete").AttemptedValue == "on" ? true : false;
            u.roleid = int.Parse(entity.GetValue("roleid").AttemptedValue);
            _userServices.UpdateUser(u);

            var user = RedisHelper.GetHashKey<Users>("Users", "Users-Id-" + u.id);
            user.isdelete = entity.GetValue("isdelete").AttemptedValue == "on" ? true : false;
            user.username = user.username + "dddddddddddddddddddd";
            var boo = RedisHelper.SetHashKey<Users>("Users", "Users-Id-" + u.id, user);//重置redis中key-value值
            //return Json(new { Success = true });
            return RedirectToAction("Index");
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