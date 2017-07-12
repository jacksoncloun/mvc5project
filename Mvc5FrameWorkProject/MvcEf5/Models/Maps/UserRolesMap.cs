using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Maps
{
    public class UserRolesMap : EntityTypeConfiguration<UserRoles>
    {
        public UserRolesMap()
        {
            this.ToTable("UserRoles");
            this.HasKey(a => a.id);
        }
    }
}
