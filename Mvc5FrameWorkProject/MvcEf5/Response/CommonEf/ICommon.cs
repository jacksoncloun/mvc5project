using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response.CommonEf
{
    public interface ICommon<T> where T : class,IEntityInterface
    {
        //T Add(T model);
        T Insert(T model);
        T Update(T model);
        void Delete(T model);
        //按主键删除,keyValues是主键值
        void Delete(params object[] keyValues);
        //keyValues是主键值
        T Find(params object[] keyValues);
        //List<T> FindAll();
        List<T> GetAll(T entity);
    }
}
