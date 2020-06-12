using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Library.Model
{
    public class Product
    {
        // identifier could be easily another data type (e.g. integral)
        // but string makes it flexible when handling different conventions
        // for product notation
        public string Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Make { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
    }
}