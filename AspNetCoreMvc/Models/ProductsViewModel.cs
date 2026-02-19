using Data.Entities;

namespace WebFormsCore.Models
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category {get;set;}

        public ProductsViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Category = product.Category;
        }
    }
}
