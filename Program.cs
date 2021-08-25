using System;

namespace ClothesShop
{
    class Program
    {
        private static ClothesShopService clshopService = new ClothesShopService();
        private const int min = 1;
        private const int max = 5;
        private const int add = 1;
        private const int edit = 2;
        private const int delete = 3;
        private const int find = 4;
        private const int exit = 5;
        public static void BuildLoginMenu(out int selected)
        {
            do
            {
                Console.WriteLine("Welcome to IAN Clothes Store!");
                Console.WriteLine("1. Sign-in");
                Console.WriteLine("2. Create a new account!");
                Console.WriteLine("3. Exit");
                Console.WriteLine("______________________");
                Console.WriteLine("Choose a function:");
                selected = Int32.Parse(Console.ReadLine());
            } while (selected < 1 || selected > 3);
        }
        public static void BuildMenuAdmin(out int selected)
        {
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("IAN Clothes Store Management System");
                Console.WriteLine("========== ADMIN FUNCTIONS ========");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Edit Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Find Product");
                Console.WriteLine("5. Exit");
                Console.WriteLine("==========================");
                Console.WriteLine("Choose a function: ");
                selected = Int32.Parse(Console.ReadLine());
            } while (selected < min || selected > max);
        }

        public static void BuildMenuClient(out int selected)
        {
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("Welcome to our Clothes Store!");
                Console.WriteLine("======================");
                Console.WriteLine("Choose the product that's you love!");
                Console.WriteLine("1. Buy Products");
                Console.WriteLine("2. Find Products");
                Console.WriteLine("3. Exit");
                Console.WriteLine("==========================");
                Console.WriteLine("Choose a function: ");
                selected = Int32.Parse(Console.ReadLine());
            } while (selected < min || selected > max);
        }

        public static void LoginMenu()
        {
            int selected = 0;
            do
            {
            x:
                BuildLoginMenu(out selected);
                Console.Clear();
                switch (selected)
                {
                    case 1:
                        Console.WriteLine("Enter username: ");
                        string user = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        string pass = Console.ReadLine();
                        var check = clshopService.Login(user, pass);
                        do
                        {
                            switch (check)
                            {
                                case "admin":
                                    clshopService.ActiveProgressBar();
                                    AdminMenu();
                                    break;
                                case "client":
                                    clshopService.ActiveProgressBar();
                                    ClientMenu();
                                    break;
                                default:
                                    Console.WriteLine("Wrong passsword ! Try again ! Press key to exit");
                                    Console.ReadKey();
                                    goto x;

                            }
                        } while (check != null);
                        break;
                    case 2:
                        Console.WriteLine("======== Create an account ===========");
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();
                        Console.Write("Enter your role (admin/client) ?: ");
                        string role = Console.ReadLine();
                        if (clshopService.CreateAccounts(username, password, role))
                        {
                            Console.WriteLine("Account created successfully!");
                            goto x;
                        }
                        else
                        {
                            Console.WriteLine("Failed to create an account ! Try again later !");
                            Console.ReadLine();
                            goto x;
                        }
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (selected != 3);
        }
        public static void ClientMenu()
        {
            int selected = 0;
            do
            {
                BuildMenuClient(out selected);
                Console.Clear();
                switch (selected)
                {
                    case add:
                        string answer;
                        var billDetails = new List<BillDetail>();
                        do
                        {
                            clshopService.ShowProduct();
                            Console.WriteLine("_______________________________________________________________________________________");
                            Console.Write("Choose Product ID to buy:");
                            var pdt_Id = int.Parse(Console.ReadLine());
                            var pdt = clshopService.FindProductById(pdt_Id);
                            Console.Write("Enter quantity: ");
                            var quantity1 = int.Parse(Console.ReadLine());
                            var bd = new BillDetail()
                            {
                                ProductPrice = pdt.productPrice,
                                ProductId = pdt.productId,
                                ProductName = pdt.productName,
                                Quantity = quantity1
                            };
                            billDetails.Add(bd);
                            Console.Write("Do you want to keep continue ? (Y/N):");
                            answer = Console.ReadLine();
                        } while (answer != "n");
                        Console.Write("");
                        clshopService.CreateBills(billDetails);
                        break;
                    case 2:
                        Console.WriteLine("Enter keyword to search: ");
                        string keyword = Console.ReadLine();
                        clshopService.FindProduct(keyword);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

            } while (selected != exit);
        }
        public static void AdminMenu()
        {
            int selected = 0;
            do
            {
                BuildMenuAdmin(out selected);
                Console.Clear();
                switch (selected)
                {
                    case add:
                        Console.WriteLine("Enter product name: ");
                        string pdName = Console.ReadLine();
                        Console.WriteLine(" Enter product price: ");
                        int pdPrice = int.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter product quantity: ");
                        int quan = int.Parse(Console.ReadLine());
                        Product pd = new Product();
                        pd.productName = pdName;
                        pd.productPrice = pdPrice;
                        pd.quantity = quan;
                        var result = clshopService.CreateProduct(pd);
                        if (result)
                        {
                            Console.Write("Product has been added successfully");
                        }
                        else
                        {
                            Console.Write("Something went wrong, please contact administrator.");
                        }
                        break;
                    case edit:
                        Console.WriteLine("Enter product ID to edit: ");
                        int editid = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Edit Product (ID {editid}) Name: ");
                        string editName = Console.ReadLine();
                        Console.WriteLine($"Edit Product (ID {editid} Price: ");
                        int editPrice = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Edit Product (ID {editid} Quantity: ");
                        int editQuantity = int.Parse(Console.ReadLine());
                        clshopService.EditProduct(editid, editName, editPrice, editQuantity);
                        break;
                    case delete:
                        Console.WriteLine("Enter Product ID to Delete: ");
                        int delId = int.Parse(Console.ReadLine());
                        clshopService.RemoveProduct(delId);
                        break;
                    case find:
                        Console.WriteLine("Enter keyword to search: ");
                        string key = Console.ReadLine();
                        clshopService.FindProduct(key);
                        break;
                    case exit:
                        Environment.Exit(0);
                        break;
                }

            } while (selected != exit);
        }

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            LoginMenu();
        }
    }
}