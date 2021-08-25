using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClothesShop
{
    class BillService : BaseService, IBillService
    {
        private string billFileName = "bills.json";
        private BillList billList = new BillList();
        public BillService()
        {
            billList = FileHelper.ReadFile<BillList>(Path.Combine(path, billFileName));
        }

        public bool CreateBill(List<BillDetail> BillDetails)
        {
            try
            {
                var billId = billList.Bills.Max(b => b.BillId) + 1;
                Bill bill = new Bill();
                bill.BillId = billId;
                bill.Date = DateTime.Now;
                bill.IsPaid = false;
                bill.BillDetails = BillDetails;

                billList.Bills.Add(bill);
                FileHelper.WriteFile<BillList>(Path.Combine(path,billFileName), billList);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        

        public Bill Find(int billId, bool isPaid = false)
        {
            return billList.Bills.FirstOrDefault(b => b.BillId == billId && b.IsPaid == isPaid);
        }
    }
}