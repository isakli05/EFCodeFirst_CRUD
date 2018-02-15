using EFCodeFirst.Model.Entity;
using EFCodeFirst.Model.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Model.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.;database=EFCodeFirst_CRUD;uid=sa;pwd=123;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Yazmış olduğumuz mapleme işlemlerini model ayarlarına(konfigürasyonlara) ekliyoruz.
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
