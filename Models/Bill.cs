
namespace ClothesShop
{
    class Bill
    {
        public int BillId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public int TotalAmount => CalsTotalAmount();
        public List<BillDetail> BillDetails { get; set; }

        private int CalsTotalAmount(){
            int total = 0;
            foreach(BillDetail bd in BillDetails){
                total += bd.Amount;
            }
            return total;
        }

        public override string ToString()
        {
            string billInfo = $"{BillId}\t\t{Date.ToString("dd/MM/yyyy hh:mm tt")}\t\t{TotalAmount}\n";
            foreach(BillDetail bd in BillDetails){
                billInfo += bd.ToString();
            }
            return billInfo;
        }
    }
}