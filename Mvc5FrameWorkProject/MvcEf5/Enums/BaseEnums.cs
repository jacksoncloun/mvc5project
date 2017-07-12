using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public enum LoginEnums
    {
        用户名不存在=0,
        用户验证失败用户名或密码错误=1,
        用户验证成功=2,
        服务器连接错误=3
    }
}
