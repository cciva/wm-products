using Shop.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class ProductViewModel
    {
        List<Category> _categories = new List<Category>();

        public ProductViewModel()
            : this(new Product(), Enumerable.Empty<Category>())
        {
            
        }

        public ProductViewModel(IEnumerable<Category> categories)
            : this(new Product(), categories)
        {

        }

        public ProductViewModel(Product product, IEnumerable<Category> categories)
        {
            Product = product;
            Categories.AddRange(categories);
            SelectedCategory = categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        public Product Product { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Category> Categories 
        { 
            get { return _categories; } 
        }
    }
}