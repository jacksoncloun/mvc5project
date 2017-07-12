using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models.Maps;

namespace Response
{
    public class DbResponse : DbContext
    {
        //private static DbResponse db = null;
        public DbResponse()
            : base("name=DbResponse")
        {
            
        }

        ///// <summary>
        ///// 使用单例模式使程序中仅存在一个实例，数据做到统一
        ///// </summary>
        ///// <returns></returns>
        //public static DbResponse getInstance()
        //{
        //    if (db != null)
        //    {
        //        db = new DbResponse();
        //    }
        //    return db;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ////移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            ////多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            ////modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Configurations.Add(new MemberConfiguration());
            //modelBuilder.Configurations.Add(new MemberExtendConfiguration());
            //modelBuilder.Configurations.Add(new MemberAddressConfiguration());
            //modelBuilder.Configurations.Add(new RoleConfiguration());
            //modelBuilder.Configurations.Add(new LoginLogConfiguration());

            modelBuilder.Configurations.Add(new UsersMap());
            modelBuilder.Configurations.Add(new RolesMap());
            modelBuilder.Configurations.Add(new UserRolesMap());

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
