using Shop.Library.Model;
using Shop.Library.Repository;
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

            // UGLY HACK : since we can not auto-generate key when inserting record into json file
            // we have to manually provide Product Id here
            RepositoryConfig rconf = ShopApp.Instance.RepositoryConfig;
            if (rconf != null && rconf.Type == RepositoryType.JsonFile)
            {
                DbResult res = ShopApp.Instance.Services
                    .Get<IRepository>()
                    .Execute<Product>(Library.Operation.Count);

                Product.Id = res.Get<int>() + 1;
            }
        }

        public Product Product { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Category> Categories 
        { 
            get { return _categories; } 
        }
    }
}