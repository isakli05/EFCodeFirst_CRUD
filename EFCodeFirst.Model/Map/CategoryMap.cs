using EFCodeFirst.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Model.Map
{
    class CategoryMap:BaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.Description).HasMaxLength(255);

            //Bir kategorinin birden çok ürüne ait olabileceği (1-M) ilişki belirtilir.
            //Product için kategori ilişkisi mecburidir.
            //CategoryID , Product tablosunda "foreign key"(ikincil anahtar) görevi görür.



            //HasMany(x => x.Products)
            //    .WithRequired(x => x.Category)
            //    .HasForeignKey(x => x.CategoryID);


        }
    }
}
