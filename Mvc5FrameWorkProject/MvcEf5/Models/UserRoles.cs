using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserRoles : IEntityInterface
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
        //public virtual Users users { get; set; }
        //public virtual Roles roles { get; set; }
    }
}
