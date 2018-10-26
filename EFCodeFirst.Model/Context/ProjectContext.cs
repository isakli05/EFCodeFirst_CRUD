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
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            //Database.Connection.ConnectionString = "Server=.;database=EFCodeFirst_CRUD;uid=sa;pwd=123;";

            Database.Connection.ConnectionString = @"Server=DESKTOP-ISA\SQLEXPRESS;Database=EFCodeFirst;Integrated Security=true";

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

        public override int SaveChanges()
        {
            var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            DateTime date = DateTime.UtcNow;

            foreach (var item in addedEntries)
            {
                Product entity = item.Entity as Product;

                if (item != null)
                {
                    if (item.State==EntityState.Added)
                    {
                    entity.AddedDate = date;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
