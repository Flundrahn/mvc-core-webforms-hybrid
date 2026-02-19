using System;

namespace Data.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }

        // Required for NHibernate proxy generation in .NET 10+
        protected internal virtual new object MemberwiseClone() => base.MemberwiseClone();
    }
}
