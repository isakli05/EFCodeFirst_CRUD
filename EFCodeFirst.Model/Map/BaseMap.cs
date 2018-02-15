using EFCodeFirst.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.Model.Map
{
    class BaseMap<T>:EntityTypeConfiguration<T> where T :BaseEntity
    {
        public BaseMap()
        {
            //using System.ComponentModel.DataAnnotations.Schema; => Eklenmelidir. 
            Property(x => x.ID).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.AddedDate).HasColumnType("datetime2").HasColumnName("AddedDate").IsOptional();
        }
    }
}
