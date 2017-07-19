using Models;
using Response;
using Response.Redis;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Response.CommonEf;

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
        public Users GetUserById(int id)
        {
            //BaseCommonEf<Users> _basece = new List<Users>();
            //var enti = _basece.Find(id);
            //return enti;
            return null;
        }

        public void UpdateUser(Users entity)
        {
            #region
            //DbEntityEntry entry = _db.Entry<Users>(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    entry.State = EntityState.Modified;// System.Data.EntityState.Unchanged;  
            //    //将实体附加到实体上下文中
            //    _db.Users.Attach(entity);
            //    _db.SaveChanges();
            //} 
            #endregion


            //_db.Set<Users>().Attach(entity);
            //_db.Entry<Users>(entity).State = EntityState.Modified;
            _db.SaveChanges();


            BaseCommonEf<Users> a = new BaseCommonEf<Users>();

            
        }
    }
}
