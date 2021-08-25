using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClothesShop
{
    class ProductService : BaseService, IProductService
    {
        private string fileName = "product.json";
        private ProductList productList = new ProductList();
        public ProductService()
        {
            productList = FileHelper.ReadFile<ProductList>(Path.Combine(path, fileName));
        }
        public bool Add(Product product)
        {
            try
            {
                int productId = productList.products.Max(p => p.productId) + 1;
                product.productId = productId;
                productList.products.Add(product);
                FileHelper.WriteFile<ProductList>(Path.Combine(path, fileName), productList);
                return true;

            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public void Delete(int Id)
        {
            Product p = productList.products.Find(p => p.productId == Id );
            if (p != null)
            {
                productList.products.Remove(p);
                Console.WriteLine("Produc has been removed sucessfully!");
                FileHelper.WriteFile<ProductList>(Path.Combine(path, fileName), productList);
            }
            else
            {
                Console.WriteLine("Can't find product ID");
            }
        }

        public bool Edit(int Id, string name, int price, int quantity)
        {
            try
            {
                foreach (Product p in productList.products)
                {
                    if (p.productId.Equals(Id))
                    {
                        p.productName = name;
                        p.productPrice = price;
                        p.quantity = quantity;
                        break;

                    }
                }
                FileHelper.WriteFile<ProductList>(Path.Combine(path, fileName), productList);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public List<Product> Find(string keyword)
        {
            var productId = 0;
            int.TryParse(keyword, out productId);
            if (productId == 0)
            {
                return productList.products.Where(p => p.productName.ToLower().Contains(keyword.ToLower())).ToList();
            }
            return productList.products.Where(p => p.productName.ToLower().Contains(keyword.ToLower()) || p.productId == productId).ToList();
        }
        public Product FindById(int Id)
        {
            return productList.products.SingleOrDefault(p => p.productId == Id);
        }
        public List<Product> Get()
        {
            return productList.products;
        }
    }

}