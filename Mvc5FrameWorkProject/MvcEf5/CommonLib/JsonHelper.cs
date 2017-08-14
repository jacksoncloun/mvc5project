using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace CommonLib
{
    public class JsonHelper
    {
        #region dataTable转换成Json格式

        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt">数据表</param>      
        /// <returns>JSON字符串</returns>      
        public static string Convert(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (jsonBuilder.Length > 1)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        #endregion

        #region DataSet转换成Json格式

        /// <summary>      
        /// DataSet转换成Json格式      
        /// </summary>      
        /// <param name="ds">DataSet</param>      
        /// <returns>JSON字符串</returns>      
        public static string Convert(DataSet ds)
        {
            StringBuilder json = new StringBuilder();
            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(Convert(dt));
                json.Append("}");
            }
            return json.ToString();
        }

        /// <summary>
        /// Json转List<T>
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="jsonText">jsonText</param>
        /// <returns></returns>
        public static List<T> GetListModel<T>(string jsonText)
        {
            List<T> list = new List<T>();
            JArray jsonlist = (JArray)JsonConvert.DeserializeObject(jsonText);
            if (jsonlist.Count() > 0)
            {
                foreach (var item in jsonlist)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
                }
            }
            return list;
        }


        /// <summary>
        /// Json字符串数据转T
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="jsonText">jsonText</param>
        /// <returns></returns>
        public static T SetModel<T>(string jsonText) where T : new()
        {
            T list = new T();
            var jsonT = JsonConvert.DeserializeObject<T>(jsonText);
            return jsonT;
        }


        /// <summary>
        /// 实体转json
        /// </summary>
        /// <param name="obj">要转换的实体对象</param>
        /// <returns></returns>
        public static string EntityToJson(object obj)
        {
            StringBuilder jsonStr = new StringBuilder();
            PropertyInfo[] pInfos = obj.GetType().GetProperties();
            string pValue = string.Empty;
            jsonStr.Append("{");
            foreach (PropertyInfo p in pInfos)
            {
                if (!(p.GetValue(obj, null) == null))
                {
                    //转义掉Json格式特殊字符 ‘\’,‘"’
                    pValue = p.GetValue(obj, null).ToString().Replace("\\", "\\\\").Replace("\"", "\\\"");
                }
                else
                {
                    pValue = string.Empty;
                }
                jsonStr.Append(string.Format("\"{0}\":\"{1}\",", p.Name, pValue));

            }
            jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("}");
            return jsonStr.ToString();
        }
        #endregion
    }
}
