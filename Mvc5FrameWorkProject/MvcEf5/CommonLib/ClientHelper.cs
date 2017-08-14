using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommonLib
{
    /// <summary>
    /// HttpClient请求帮助类
    /// </summary>
    public class ClientHelper
    {
        private static String _apiBaseAddress = "https://exam-anubis.ele.me/anubis-webapi/v2/order";        

        /// <summary>
        /// HttpClient请求
        /// </summary>
        /// <param name="api"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Task<string> InvokePostRequest(string api, Dictionary<string, string> arg)
        {
            _apiBaseAddress = api;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();

                //SsoUserInfo userInfo = ParamHelper.GetUserInfo().Result;
                //if (userInfo != null && !userInfo.AccessToken.IsNullOrWhiteSpace())
                //{
                //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userInfo.AccessToken);
                //}

                client.Timeout = TimeSpan.FromSeconds(720);
                var response = client.PostAsync(api, new FormUrlEncodedContent(arg)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Task.Factory.StartNew<string>(() =>
                    {
                        return result;
                    });
                }
                throw new HttpResponseException(response);
            }
        }



    }
}
