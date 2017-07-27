using Models;
using Services.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Response.Redis;
using Enums;
using System.Reflection;
using System.IO;

namespace MvcEf5.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var returns = WebSession.GetSetLoginSession() ?? new LoginResult();
            ViewBag.LoginMessage = returns;

            List<Roles> roles = RedisHelper.HashGetAll<Roles>("Roles");
            List<SelectListItem> roleslist = getlistItems(roles);
            ViewBag.Roles = roles;
            ViewBag.RolesList = roleslist;
        }

        /// <summary>
        /// 错误处理并且打日志
        /// 其他所有的Controller继承该BaseController即可
        /// 在这个Controller中重写OnException方法可以记录日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
            // 通过filterContext.Exception来获取这个异常。'
            string directory = @"D:\Temp\";
            string filePath = directory + @"Exceptions.txt";
            DirectoryInfo di = new DirectoryInfo(directory);
            int ar = filterContext.Exception.Message.Length;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath, ar, FileOptions.Asynchronous);
            }
            else
            {
                StreamWriter sw = System.IO.File.AppendText(filePath);
                sw.WriteLine(filterContext.Exception.Message);
                sw.Close();
            }
            // 执行基类中的OnException
            //base.OnException(filterContext);

            // 标记异常已处理
            filterContext.ExceptionHandled = true;
            // 跳转到错误页
            filterContext.Result = new RedirectResult("/Views/Shared/Error4043.html");  
            //Url.Action("Index", "Error")   
            //可以通过throw new excption当控制器错误信息出现的时候指定错误信息返回到这里    
            //可以将ErrorController中的这个错误页面的视图方法多加一个参数,将上面的错误信息传递到error的方法里面就可以在页面上直接显示出来    
            //可以用枚举进行判断消息是我们自定义的消息或者是系统出错,来判断是否可以显示错误信息在页面上
        }

        /// <summary>
        /// 将某个List<T>集合转换为List<SelectListItem>
        /// 实体类T中需要添加两个字段Text,Value,可参考实体类Roles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lister"></param>
        /// <returns></returns>
        public List<SelectListItem> getlistItems<T>(List<T> lister)
        {
            List<SelectListItem> arr = new List<SelectListItem>();
            foreach (var item in lister)
            {
                var ttype = item.GetType();
                var tproperty = ttype.GetProperty("Text");
                var vproperty = ttype.GetProperty("Value");
                var text = tproperty.GetValue(item, null).ToString();
                var value = vproperty.GetValue(item, null).ToString();
                arr.Add(new SelectListItem() { Text = text, Value = value });
            }
            return arr;
        }

        /// <summary>
        /// 公共方法：根据枚举类的值找到这个值对应的名称并且返回   
        /// 如枚举  张三=1,李四=2,王五=3   含义：名称=value值   调用该方法传参2-type传参0返回李四  调用该方法传参李四-type=1返回字符串2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cva">默认value值,则取出名称,也可传入名称返回value值</param>
        /// <param name="entity">枚举类</param>
        /// <param name="type">0传入value值并返回名称   1传入名称返回value值</param>
        /// <returns></returns>
        public string getClassNames<T>(string cva, T entity, int type = 0)
        {
            Type tp = entity.GetType();
            Array pproperty = tp.GetEnumValues();
            foreach (object item in pproperty)
            {
                if (type == 0 && (int)item == int.Parse(cva))
                {
                    return item.ToString();         //返回枚举的名称
                }
                if (type == 1 && item.ToString() == cva)
                {
                    return ((int)item).ToString();  //返回枚举的值
                }
            }
            return "";
        }






    }
}