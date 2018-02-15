using EFCodeFirst.Model.Enum;
using System;

namespace EFCodeFirst.Model.Entity
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public Status Status { get; set; }
    }
}
