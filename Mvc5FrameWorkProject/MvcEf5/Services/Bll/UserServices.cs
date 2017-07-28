using Models;
using Response;
using Response.Redis;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

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
            return _db.Users.Where(a => a.id == id).FirstOrDefault();
            //return null;
        }

        public void UpdateUser(Users entity)
        {
            #region 使用这种方法如果这个实体中有一些数据默认为空值，那么也会默认保存进数据库
            //DbEntityEntry entry = _db.Entry<Users>(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    entry.State = EntityState.Modified;// System.Data.EntityState.Unchanged;  
            //    //将实体附加到实体上下文中
            //    _db.Users.Attach(entity);
            //    _db.SaveChanges();
            //} 
            #endregion

            _db.SaveChanges();
            entity.username = entity.username + "rrrrrrrr";
            var boo = RedisHelper.SetHashKey<Users>("Users", "Users-Id-" + entity.id, entity);//重置redis中key-value值
            //Redis中存储的是  [Users:[Users-id-1:{},Users-id-2:{},Users-id-3:{}]]  这种格式，所以需要修改设置的是  Users对应的 Users-id-3 这个键的值
        }

        public void DeleteUser(int id)
        {
            #region 删除1直接使用sql进行删除
            //using (var md = new DbResponse())
            //{
            //    md.Database.ExecuteSqlCommand("Delete * from Users where id=" + id);
            //}
            #endregion 
            RedisHelper.DeleteHase("Users", "Users-Id-" + id);
            _db.Users.Remove(_db.Users.Where(a => a.id == id).FirstOrDefault());
            _db.SaveChanges();
        }
    }
}
