using EFCodeFirst.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Model.Map
{
    class ProductMap:BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");
            Property(x => x.Price).IsOptional();
            Property(x => x.UnitsInStock).IsOptional();
            Property(x=>x.Quantity).IsOptional();

            //Product'ın Category'si zorunludur.
            //one to many ilişkisi için ".withMany" kullanılır, ve kategorinin birçok ürünü olabileceği belirtilir.
            //foreign key(ikincil anahtar) olan "CategoryID" belirtilir.
            //İlişkisel veritabanlarında bir tablodaki değer silinince diğer tablodaki karşılığı silinmesin istenirse WillCascadeOnDelete(false) yapılmalıdır.

            HasRequired(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryID)
                .WillCascadeOnDelete(false);
        }
    }
}
