using System;
namespace ClothesShop
{
    class ClothesShopService
    {
        private readonly ProductService productService;
        private readonly BillService billService;

        private readonly LoginService loginService;

        public ClothesShopService()
        {
            productService = new ProductService();
            billService = new BillService();
            loginService = new LoginService();
        }

        public void ActiveProgressBar()
        {
            Console.Write("Loading... ");
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(25);
                }
            }
            Console.WriteLine("Done.");
        }


        public bool CreateProduct(Product product)
        {
            return productService.Add(product);
        }

        public void RemoveProduct(int Id)
        {
            productService.Delete(Id);
        }

        public bool EditProduct(int Id, string name, int price, int quantity)
        {
            return productService.Edit(Id, name, price, quantity);
        }
        public void ShowProduct()
        {
            Console.WriteLine("ID\t\t\tName\t\t\tPrice\t\t\tQuantity");
            Console.WriteLine("_______________________________________________________________________________________");
            foreach (Product product in productService.Get())
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void ShowShirts()
        {
            Console.WriteLine("ID\t\t\tName\t\t\tPrice\t\t\tQuantity");
            Console.WriteLine("_______________________________________________________________________________________");
        }

        public void ShowPants()
        {

        }

        public void ShowShoes()
        {

        }
        public void FindProduct(string keyword)
        {
            var products = productService.Find(keyword);
            if (products == null || products.Count == 0)
            {
                Console.Write("Not found.");
            }
            else
            {
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
            }
        }

        public Product FindProductById(int productId)
        {
            return productService.FindById(productId);
        }

        #region Bill methods
        public void FindBill(int billId, bool isPaid = false)
        {
            var bill = billService.Find(billId);
            if (bill == null)
            {
                Console.Write("Not found");
            }
            else
            {
                Console.Write(bill.ToString());
            }
        }

        public bool CreateBills(List<BillDetail> BillDetails)
        {
            return billService.CreateBill(BillDetails);
        }



        #endregion

        #region Login methods
        public bool CreateAccounts(string username, string password, string role)
        {
            return loginService.CreateAccount(username, password, role);
        }
        public string Login(string user, string pass)
        {

            Account acc = loginService.GetAccount(user);
            if (acc == null)
            {
                Console.WriteLine("Not found account in systems! Please try again !");

            }
            else
            {

                if (acc.password == pass)
                {

                    switch (acc.role)
                    {
                        case "admin":
                            return "admin";
                        case "client":
                            return "client";
                        default:
                            return null;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}