using Models;
using Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response.CommonEf
{
    /// <summary>
    /// 在ef的基础上进行封装一个savechange事件，实际的用处并不大，只是看着会简洁一些，然而设计上会导致循环引用，
    /// 比如  models引用response  response引用models 除非将model和response写在一个项目里面但是意义不大，这里的内容可用作学习不同的实现方式
    /// 使用这个抽象泛型类的时候 直接让实体model如Users  派生自BaseCommonEf<Users>  即可，因为抽象类和抽象接口不能进行实例化所以引用的时候直接 Users u=new Users();即可如果是接口  ICommon cm=new Common();
    /// 使用方法可参考下面的例子
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCommonEf<T> : ICommon<T> where T : class,IEntityInterface
    {
        DbResponse _db;
        public BaseCommonEf()
        {
            _db = new DbResponse();
        }

        public T Insert(T model)
        {
            _db.Set<T>().Add(model);
            _db.SaveChanges();
            return model;
        }

        public T Update(T model)
        {
            if (_db.Entry<T>(model).State == EntityState.Modified)
            {
                _db.SaveChanges();
            }
            else if (_db.Entry<T>(model).State == EntityState.Detached)
            {
                try
                {
                    _db.Set<T>().Attach(model);
                    _db.Entry<T>(model).State = EntityState.Modified;
                }
                catch (InvalidOperationException)
                {
                    //T old = _db.Set<T>().Find(model.Id);    //Id是在 IEntityInterface 存在的一个字段
                    //T old=Find(model.Id);
                    //_db.Entry(model).CurrentValues.SetValues(model);
                }
                _db.SaveChanges();
            }
            return model;
        }
        public T Find(params object[] keyValues)
        {
            return _db.Set<T>().Find(keyValues);
        }
        public void Delete(T model)
        {
            try
            {
                _db.Set<T>().Remove(model);
                _db.SaveChanges();
            }
            catch (InvalidOperationException)
            { }
        }
        public void Delete(params object[] keyValues)
        {
            T model = Find(keyValues);
            if (model != null)
            {
                _db.Set<T>().Remove(model);
                _db.SaveChanges();
            }  
        }
        public List<T> GetAll(T entity)
        {
            return _db.Set<T>().ToList();
        }
    }
    public class Hvi : BaseCommonEf<Hvi>
    {
        public Hvi()
        { 
        
        }

    }
    public class Hello
    {
        public void active()
        {
            Hvi h = new Hvi();
            
        }
    }
}
