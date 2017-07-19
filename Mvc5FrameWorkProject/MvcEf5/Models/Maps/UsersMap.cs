using Models.BaseInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Maps
{
    public class UsersMap : EntityTypeConfiguration<Users>, IEntityMapper
    {
        public UsersMap()
        {
            this.ToTable("Users");
            this.HasKey(a => a.id);

            //HasRequired(r => r.Roles).WithMany().HasForeignKey(r => r.roleid);//Map(m => m.ToTable("Roles"));
        }
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
