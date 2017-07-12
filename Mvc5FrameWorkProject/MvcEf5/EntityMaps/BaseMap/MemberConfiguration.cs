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
    /// 用户信息映射类，用户信息是关系的主体，所有的关系都不在此映射类中进行配置
    /// </summary>
    public class MemberConfiguration : EntityTypeConfiguration<Member>
    {
    }
}
