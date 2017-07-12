using EntityMaps.BaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMaps.BaseMap
{
    /// <summary>
    /// 登录记录信息映射，配置登录信息与用户信息的 N:1 的关系
    /// </summary>
    public class LoginLogConfiguration : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogConfiguration()
        {
            HasRequired(m => m.Member).WithMany(n => n.LoginLogs);
        }
    }
}
