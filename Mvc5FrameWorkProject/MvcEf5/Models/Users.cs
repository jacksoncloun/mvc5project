using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Users
    {
        //private ICollection<Roles> _roles;
        public Users()
        { }

        public int id { get; set; }
        public int? userid { get; set; }
        public string username { get; set; }
        public string userpwd { get; set; }
        public bool isdelete { get; set; }
        public int? roleid { get; set; }

        //public virtual ICollection<Roles> Roles { get; set; }
        /// <summary>
        /// 获取或设置用户所对应的权限的集合
        /// </summary>
        //public virtual ICollection<Roles> Roles
        //{
        //    get { return _roles ?? (_roles = new List<Roles>()); }
        //    protected set { _roles = value; }
        //}
    }
}
