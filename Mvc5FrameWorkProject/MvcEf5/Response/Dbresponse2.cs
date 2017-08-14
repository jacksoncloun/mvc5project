using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response
{
    public class Dbresponse2 : DbContext
    {
        public Dbresponse2()
            : base("name=DbResponse")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Users> Users { get; set; }
    }
}
