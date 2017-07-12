using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Roles
    {
        public int id { get; set; }
        public string rolename { get; set; }
        public int? roletype { get; set; }
        public bool isdelete { get; set; }
        public int? proleid { get; set; }
        public int? roleleavel { get; set; }
        //public Users Users { get; set; }

        /// <summary>
        /// 获取或设置权限所对应的用户的集合
        /// </summary>
        //public virtual ICollection<Users> Users { get; set; }
    }
}
