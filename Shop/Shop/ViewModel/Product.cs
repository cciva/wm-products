using Shop.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.ViewModel
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}