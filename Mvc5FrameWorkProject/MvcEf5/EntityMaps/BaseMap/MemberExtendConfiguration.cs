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
    /// 用户扩展信息映射类，配置用户扩展信息与用户信息的 0:1 关系
    /// </summary>
    public class MemberExtendConfiguration : EntityTypeConfiguration<MemberExtend>
    {
        public MemberExtendConfiguration()
        {
            HasRequired(m => m.Member).WithOptional(n => n.Extend);
        }
    }
}
