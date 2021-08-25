namespace ClothesShop
{
    class BillDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int Amount => ProductPrice * Quantity;
        public override string ToString()
        {
            return $"\t\t{ProductId}\t\t{ProductName}\t\t{ProductPrice}\t\t{Quantity}\t\t{Amount}\n";
        }
    }
}

