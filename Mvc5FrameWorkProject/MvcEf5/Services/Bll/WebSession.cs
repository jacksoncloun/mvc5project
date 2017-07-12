using Models;
using Response;
using Response.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class WebSession
    {
        public WebSession()
        {

        }

        public static void SetLoginSession(LoginResult lr)
        {
            if (lr != null)
            {
                System.Web.HttpContext.Current.Session["User"] = lr;
            }
        }
        public static LoginResult GetSetLoginSession()
        {
            return (LoginResult)System.Web.HttpContext.Current.Session["User"] ?? new LoginResult();
        }

        public static void SessionClear()
        {
            System.Web.HttpContext.Current.Session.Clear();
        }        
    }
}
