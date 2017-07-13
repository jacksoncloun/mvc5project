using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// 泛型约束示例  泛型可以约束类  泛型可以约束方法
    /// 委托简单委托和复杂委托
    /// </summary>
    public class GenericT
    {
        public GenericT()
        {

        }

        #region 泛型方法约束、泛型方法约束委托
        /// <summary>
        /// 泛型约束  可以约束泛型方法中的泛型类别
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void shift<T>(T entity)
            where T : IBaseClass { }
        /// <summary>
        /// 泛型约束  泛型委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sher"></param>
        /// <returns></returns>
        public delegate T control<T>(string sher)
            where T : IBaseClass, new();
        #endregion

        #region 泛型委托约束、泛型方法约束委托的使用方法
        //public event control<T> _control;
        public void lowed()
        {
            //调用泛型委托
            control<BaseClass> cr = new control<BaseClass>(lowfunc);
            cr("");


            Ilove<BaseClass> l = new Ilove<BaseClass>();   //约束类型  ICollection派生自IEnumerable    所以当BaseT今派生自ICollection调用Ilove泛型不会报错

            //  IDictionary(派生自)-->ICollection(派生自)-->IEnumerable
            //  List(派生自)-->IList(派生自)-->ICollection(派生自)-->IEnumerable

            BaseClass b1 = new BaseClass();
            BaseClass b2 = new BaseClass();
            Ilove<BaseClass, BaseClass> arlove = new Ilove<BaseClass, BaseClass>();
            arlove.sherter(b1, b2);
        }
        public BaseClass lowfunc(string sh)
        {
            BaseClass _BaseT = new BaseClass();
            _BaseT.shfter = sh;
            return _BaseT;
        }
        #endregion

        #region //简单的委托以及委托的调用方法  以理发为例子  理发师傅不同那么做的事情也不同,把理发这个动作封装起来
                //理发师傅不同调用的理发方法也不同,如果以后增加了理发师傅直接新增一个方法即可    也类似于简单工厂模式,如果将每个人的理发方法单独写在类中就是工厂模式
        /// <summary>
        /// 通用简单委托
        /// </summary>
        /// <param name="herb"></param>
        public delegate void retry(string herb);
        //public retry _retry;
        public void retryMain()
        {
            //_retry("胡萝卜");
            //_retry = retryfun;
            //_retry += retryfun2;

            DelegateCenter dc = new DelegateCenter();
            dc._retry += retryfun;       //将方法绑定到委托
            dc._retry += retryfun2;
            dc._DelegateCenter("理发");  //调用事件中心执行事件操作
        }
        public void retryfun(string herb)
        {
            Console.Write("张三" + herb);
        }
        public void retryfun2(string herb)
        {
            Console.Write("李四" + herb);
        }
        #endregion
    }

    #region 简单委托事件调用中心
    public class DelegateCenter
    {
        public event Services.GenericT.retry _retry;  //_retry和普通的变量一样,并非每次都是Public类型   使用event可以自动识别
        public void _DelegateCenter(string herb)
        {
            if (_retry != null)
            {
                _retry(herb);
            }
        }
    }
    #endregion

    #region 简单的泛型类约束
    public class Ilove<T>
        where T : IBaseClass, new()
    {
        T t = new T();
    }
    public class Ilove<TKey, TValue>
        where TKey : IBaseClass
        where TValue : IBaseClass
    {
        public void sherter(TKey a, TValue b)
        { }
    }
    #endregion

    #region 复杂的泛型类约束

    /// <summary>
    /// 泛型类  泛型约束
    /// where用于指定类型约束,这些约束可以作为泛型声明中定义的类型参数的变量    
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Message<T>
        where T : IComparable    //泛型接口约束,T只能是继承自IComparable接口的类
    {

    }

    /// <summary>
    ///  泛型类  泛型约束
    ///  除了接口约束,where还可以包括基类约束,以指出某个类型必须将指定的类作为基类(或者就是该类本身),才能用作该泛型的类型参数.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class Message<T, U>      //基类约束
        where T : class
        where U : struct
    {

    }


    /// <summary>
    /// 泛型类  泛型约束
    /// where还可以包括构造函数约束,可以使用new运算符创建类型参数的实例;但类型参数必须受构造函数约束new()的约束.new()约束可以让编译器知道:提供的任何类型参数都必须具有可访问的无参数构造函数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Message1<T>
      where T : IComparable, new()    //构造函数约束,new()约束放在所有约束最后,使用new()约束可以让T创建实例
    {
        T cs = new T();
    }

    /// <summary>
    /// 泛型类  泛型约束
    /// 对于多个类型参数,每个类型参数都使用一个where
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Dictionary<TKey, TValue>
        where TKey : IComparable, IEnumerable
        where TValue : IComparable
    {
        public void add(TKey a, TValue b)
        { }
    }
    #endregion


    /// <summary>
    /// 调用泛型类或者泛型方法时使用的基本类
    /// </summary>
    public class BaseClass : IBaseClass
    {
        public string shfter { get; set; }
    }

    public interface IBaseClass { }
}
