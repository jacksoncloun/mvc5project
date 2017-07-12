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
    /// 用户地址信息映射类，配置用户地址信息的复杂类型映射，复杂类型继承于 ComplexTypeConfiguration<>
    /// </summary>
    public class MemberAddressConfiguration : ComplexTypeConfiguration<MemberAddress>
    {
        public MemberAddressConfiguration()
        {
            Property(m => m.Province).HasColumnName("Province");
            Property(m => m.City).HasColumnName("City");
            Property(m => m.County).HasColumnName("County");
            Property(m => m.Street).HasColumnName("Street");
        }
    }
}
