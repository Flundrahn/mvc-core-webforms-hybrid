using System.Collections.Generic;

namespace Data.Entities
{
    internal class Shelf
    {
        public virtual int Id { get; set; }
        public virtual IList<Product> Products { get; set; }

        public Shelf()
        {
            Products = new List<Product>();
        }
    }
}
