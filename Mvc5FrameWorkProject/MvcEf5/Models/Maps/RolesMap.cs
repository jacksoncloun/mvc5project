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
    public class RolesMap : EntityTypeConfiguration<Roles>, IEntityMapper
    {
        public RolesMap()
        {
            this.ToTable("Roles");
            this.HasKey(a => a.id);

            //HasRequired(r => r.Users).WithMany(p => p.Roles).HasForeignKey(r => r.id);   

        }
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
