using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLib
{
    public class GaodeApi
    {
        public readonly string key;
        public GaodeApi(string inkey)
        {
            key = inkey;
        }
        /// <summary>
        /// 根据地址获取地址的经纬度所属省市等等
        /// </summary>
        /// <param name="inaddress">需要翻译的地址</param>
        /// <returns></returns>
        public string getgeo(string inaddress)
        {
            var url = "http://restapi.amap.com/v3/geocode/geo";
            var param = string.Format("key={0}&address={1}", key, inaddress);
            var data = RequestHelper.Request(url, param, "POST", "application/json");
            return data;
        }
    }
}
