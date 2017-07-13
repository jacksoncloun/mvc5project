using Models;
using Response;
using Response.Redis;
using Services.IBll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class RedisService : IRedisService
    {
        DbResponse _db;
        public RedisService()
        {
            if (_db == null)
            {
                _db = new DbResponse();
            }
        }

        /// <summary>
        /// 缓存Users表到redis数据库
        /// </summary>
        public void SaveInRedis()
        {
            RedisHelper.KeyDelete("Users");
            bool listusers = RedisHelper.KeyExists("Users");
            if (!listusers)
            {
                //UsersInredis(); RolesInredis();
                List<Users> users = _db.Users.ToList();
                Inredis<Users>(users, "Users");

                List<Roles> Roles = _db.Roles.ToList();
                Inredis<Roles>(Roles, "Roles");
            }
        }


        public void Inredis<T>(List<T> users, string key)
        {
            Func<T, string> arr = new Func<T, string>(getfuncommon);
            RedisHelper.HashSet<T>(key, users, arr);
        }
        public string getfuncommon<T>(T entity)
        {
            return (entity.GetType()).BaseType.Name + "-Id-" + getPropertyvalue<T>(entity, "id");
        }
        /// <summary>
        /// 泛型方法,通过反射获取实体类中的某个字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="membername"></param>
        /// <returns></returns>
        public string getPropertyvalue<T>(T entity, string membername)
        {
            Type type = entity.GetType();//typeof(T)

            PropertyInfo property = type.GetProperty(membername);

            if (property == null) return string.Empty;

            object o = property.GetValue(entity, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
    }
}
