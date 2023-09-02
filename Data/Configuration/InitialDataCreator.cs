using System.Collections.Generic;
using Data.Entities;
using Data.Interfaces;

namespace Data.Configuration
{
    public class InitialDataCreator
    {
        private readonly IProductRepository _productRepo;

        public InitialDataCreator(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public void AddProducts()
        {
            foreach (Product product in CreateProducts())
            {
                _productRepo.Save(product);
            }
        }

        private Product[] CreateProducts()
        {
            return new Product[]
            {
                new Product {Name = "Melon", Category = "Fruits"},
                new Product {Name = "Pear", Category = "Fruits"},
                new Product {Name = "Milk", Category = "Beverages"},
                new Product {Name = "Coca Cola", Category = "Beverages"},
                new Product {Name = "Pepsi Cola", Category = "Beverages"},
            };
        }

        //private Shelf[] CreateShelves()
        //{
        //    return new Shelf[]
        //    {
        //        new Shelf{Products = new List<Product> {CreateProducts()[0], CreateProducts()[1]}},
        //        new Shelf{Products = new List<Product> {CreateProducts()[2], CreateProducts()[3], CreateProducts()[4]}}
        //    };
        //}
    }
}
