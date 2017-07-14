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
        /// <param name="bi">默认value值,则取出名称</param>
        /// <param name="en">枚举类</param>
        /// <param name="type">0传入value值并返回名称   1传入名称返回value值</param>
        /// <returns></returns>
        public string getClassNames<T>(string bi, T en, int type = 0)
        {
            Type tp = en.GetType();
            Array pproperty = tp.GetEnumValues();
            foreach (object item in pproperty)
            {
                if (type == 0 && (int)item == int.Parse(bi))
                {
                    return item.ToString();         //返回枚举的名称
                }
                if (type == 1 && item.ToString() == bi)
                {
                    return ((int)item).ToString();  //返回枚举的值
                }
            }
            return "";
        }
    }
}