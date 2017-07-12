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
    public class UserServices : IUserServices
    {
        DbResponse _db;
        public UserServices()
        {
            _db = new DbResponse();
        }

        public List<Users> getAlluser()
        {
            List<Users> user = RedisHelper.HashGetAll<Users>("Users");
            //var user = from a in _db.Users
            //           select a;
            return user.ToList();
        }

        
    }
}
