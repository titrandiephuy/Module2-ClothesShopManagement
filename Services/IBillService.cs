using System.Collections.Generic;

namespace ClothesShop
{
    interface IBillService
    {
        Bill Find (int billId, bool isPaid = false);
        bool CreateBill(List<BillDetail> BillDetails);
    }
}