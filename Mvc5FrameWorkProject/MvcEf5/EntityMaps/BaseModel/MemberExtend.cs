using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityMaps.BaseModel
{
    /// <summary>
    ///     实体类——用户扩展信息
    /// </summary>
    [Description("用户扩展信息")]
    public class MemberExtend : BaseEntity
    {
        /// <summary>
        /// 初始化一个 用户扩展实体类 的新实例
        /// </summary>
        public MemberExtend()
        {
            Id = CombHelper.NewComb();
        }

        public Guid Id { get; set; }

        public string Tel { get; set; }

        public MemberAddress Address { get; set; }

        public virtual Member Member { get; set; }
    }
}
