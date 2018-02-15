using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Model.Entity
{
    public class Product:BaseEntity
    {
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public short UnitsInStock { get; set; }


        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
