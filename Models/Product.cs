using System;
using System.Collections.Generic;

namespace ClothesShop
{
    class Product
    {
        public int productId { get; set; }

        // public int productType { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int quantity { get; set; }
        public override string ToString()
        {
            return string.Format($"{productId,-20}{productName,-30}{productPrice,-20}{quantity,-20}");
        }
    }

    class ProductList
    {
        public List<Product> products { get; set; }
    }
}