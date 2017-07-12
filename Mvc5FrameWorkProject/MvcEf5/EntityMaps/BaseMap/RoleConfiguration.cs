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
    /// 角色信息映射，配置角色信息与用户信息的 N:N 的关系
    /// </summary>
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasMany(m => m.Members).WithMany(n => n.Roles);
        }
    }
}
