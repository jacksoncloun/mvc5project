using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginResult
    {
        public LoginResult()
        {
            user = new Users();
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public Users user { get; set; }
        /// <summary>
        /// 是否处于已登录状态
        /// </summary>
        public bool issuccess { get; set; }
        /// <summary>
        /// 登录操作后的描述信息
        /// </summary>
        public string decription { get; set; }
    }
}
