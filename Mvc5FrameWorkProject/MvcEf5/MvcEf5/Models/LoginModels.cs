using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEf5.Models
{
    public class LoginModels
    {
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string LoginVerify { get; set; }
    }
}