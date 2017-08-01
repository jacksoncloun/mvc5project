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
using System.Reflection;
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

        #region 错误处理
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
        #endregion

        public ActionResult Index()
        {
            List<Users> model = RedisHelper.HashGetAll<Users>("Users");
            return View(model);
        }

        #region session clear
        /// <summary>
        /// 清理掉session 验证控制器的是否已登录的权限验证
        /// </summary>
        /// <returns></returns>
        public ActionResult Clear()
        {
            WebSession.SessionClear();
            return RedirectToAction("Index", "Home");
        }

        #endregion
        #region 编辑以及保存
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
        /// <summary>
        /// 修改操作，不仅修改数据库中的数据值，还需要修改存储在redis中的数据值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(FormCollection entity)
        {

            Users u = _userServices.GetUserById(int.Parse(entity.GetValue("id").AttemptedValue)); //new Users();
            //u.id = int.Parse(entity.GetValue("id").AttemptedValue);
            u.username = entity.GetValue("username").AttemptedValue;
            u.isdelete = entity.GetValue("isdelete") == null ? false : true;  // .AttemptedValue == "on" ? true : false;
            u.roleid = int.Parse(entity.GetValue("roleid").AttemptedValue);
            _userServices.UpdateUser(u);

            return RedirectToAction("Index");
        }
        #endregion

        #region 公共删除方法
        public ActionResult CommonDel(string N, int id)
        {
            EntitiesNames en = new EntitiesNames();
            var currClassname = getClassNames<EntitiesNames>(N, en, 0);  //这里获取了类的名称，将返回值修改为返回类名         
            Assembly[] AssbyCustmList = System.AppDomain.CurrentDomain.GetAssemblies();//查找所有的程序集
            foreach (Assembly item in AssbyCustmList)
            {
                var b = item.FullName;
                var e = item.GetType();
                var f = item.GetTypes();
                var g = item.Modules;
                var h = item.GetManifestResourceNames();                
                var items = item.GetTypes().Where(a => a.Name == "Users").FirstOrDefault(); //程序集里面的对象
                if (items != null)
                {
                    var c = item.GetTypes().Where(a => a.Name == "Users");
                    var d = item.CreateInstance("Models.Users");
                }
            }
            //var AssemblyList = Assembly.GetCallingAssembly();

            string assemblyname = "";
            Assembly.LoadFrom(assemblyname);

            //_userServices.DeleteUser(id);
            return View();
        }
        #endregion

        #region
        public ActionResult Del(int id)
        {
            _userServices.DeleteUser(id);
            return View();
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
        #endregion





    }

    #region C# 利用反射根据类名创建类的实例对象
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return (T)obj;//类型转换并返回
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
    #endregion

}