using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Library.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Make { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class ProductCollection : List<Product>
    {

    }
}