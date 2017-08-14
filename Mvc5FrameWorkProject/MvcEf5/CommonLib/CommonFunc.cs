using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class CommonFunc
    {
        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns>时间格式</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtDateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ltime = long.Parse(timeStamp);
            dtDateTime = dtDateTime.AddMilliseconds(ltime).ToLocalTime();
            return dtDateTime;
        }
        /// 将c# DateTime时间格式转换为Unix时间戳格式 
        /// </summary> 
        /// <param name="time">时间</param> 
        /// <returns>long</returns> 
        public static long ConvertDateTimeToInt(string time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (DateTime.Parse(time).Ticks - startTime.Ticks) / 10000;   //除10000调整为13位     
            return t;
        }

        public static int RandomNum()
        {
            Random r = new Random();
            var salt = r.Next(1000, 9999);
            return salt;
        }
    }
}
