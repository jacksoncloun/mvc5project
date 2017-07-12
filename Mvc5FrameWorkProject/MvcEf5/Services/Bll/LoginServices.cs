using Enums;
using Models;
using Response;
using Response.Redis;
using Services.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class LoginServices : ILoginServices
    {
        DbResponse _db; 
        public LoginServices()
        {
            _db = new DbResponse(); 
        }

        public LoginResult Loginres(Users entity)
        {
            LoginResult lr = new LoginResult();
            lr.issuccess = false;
            lr.decription = LoginEnums.用户验证失败用户名或密码错误.ToString();
            try
            {                
                var checkpd = _db.Users.Where(a => a.username == entity.username && a.userpwd == entity.userpwd).FirstOrDefault();
                if (checkpd != null)
                {
                    lr.user = checkpd;
                    lr.issuccess = true;
                    lr.decription = LoginEnums.用户验证成功.ToString();
                    WebSession.SetLoginSession(lr);
                }
            }
            catch
            {
                lr.decription = LoginEnums.服务器连接错误.ToString();
            }
            return lr;
        }
    }
}
