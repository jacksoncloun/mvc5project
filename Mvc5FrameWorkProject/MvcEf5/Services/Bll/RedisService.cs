using Models;
using Response;
using Response.Redis;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void SaveInRedis()
        {
            RedisHelper.KeyDelete("Users");
            bool listusers = RedisHelper.KeyExists("Users");
            if (!listusers)
            {
                List<Users> users = _db.Users.ToList();
                //RedisHelper.SetStringKey("Users", users);

                Func<Users, string> arr = new Func<Users, string>(getfun);
                RedisHelper.HashSet<Users>("Users", users, arr);
            }
        }

        public string getfun(Users entity)
        {
            return "Users-Id-" + entity.id.ToString();//"Users11";
        }



        private delegate string Say(string b);
        public static string SayHello1(string a)
        {
            return "Hello" + a;
        }
        public void Main1()
        {
            Say say = SayHello1;
            var str = say("d");
        }






        public static string SayHello2()
        {
            return "Hello";
        }
        public void Main2()
        {
            Func<string> say = SayHello2;
            var d = say();
        }





        public static string SayHello3(string str)
        {
            return str + str;
        }
        public void Main3()
        {
            Func<string, string> say = SayHello3;
            string str = say("abc");
        }
    }
}
