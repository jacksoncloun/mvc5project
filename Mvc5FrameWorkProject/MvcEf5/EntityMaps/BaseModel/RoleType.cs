using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMaps.BaseModel
{
    /// <summary>
    /// 表示角色类型的枚举
    /// </summary>
    [Description("角色类型")]
    public enum RoleType
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户角色")]
        User = 0,

        /// <summary>
        /// 管理员类型
        /// </summary>
        [Description("管理角色")]
        Admin = 1
    }
}
