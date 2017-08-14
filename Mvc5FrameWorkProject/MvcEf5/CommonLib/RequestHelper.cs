using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace CommonLib
{
    /// <summary>
    /// HttpRequest请求帮助类
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="method">请求方式</param>
        /// <param name="url">请求地址</param>
        /// <param name="postData">提交数据</param>
        /// <returns>返回的结果</returns>
        public static string Request(string url, string postData, string method = "POST", string ContentType = "application/json")
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            if (method == "POST")
            {
                webRequest.ContentType = ContentType;
                webRequest.Timeout = 300000;
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                finally
                {
                    if (requestWriter != null)
                    {
                        requestWriter.Close();
                        requestWriter = null;
                    }
                }
            }
            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;
        }

        /// <summary>
        /// 获取响应
        /// </summary>
        /// <param name="webRequest"></param>
        /// <returns></returns>
        public static string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            HttpWebResponse httpWebResponse = null;
            string responseData = "";
            Stream stream = null;
            try
            {
                httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                stream = httpWebResponse.GetResponseStream();
                using (responseReader = new StreamReader(stream, Encoding.UTF8))
                {
                    responseData = responseReader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                    httpWebResponse = null;
                }
            }
            return responseData;
        }

        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="urlPath">请求的URLPath</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <returns>
        /// 字符串res
        /// </returns>
        public static string GetResponse(string urlPath, string json)
        {
            string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            if (string.IsNullOrEmpty(urlPath))
                throw new ArgumentNullException("urlPath");

            string res = "";
            string url = urlPath;
            HttpWebRequest request = null;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = DefaultUserAgent;
            request.Timeout = 300000;

            byte[] data = Encoding.UTF8.GetBytes(json);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        res = sr.ReadToEnd();
                        sr.Close();
                    }
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                res = "{\"m\":-1,\"_e\":\"调用API时出错:" + ex.Message + "\"}";
            }
            return res;
        }

    }
}
