using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public void CheckOut(int id, int discount, float totalPrice)
        {
            string SQL = "Update dbo.Bill Set DateCheckOut = GETDATE() , Status = 1 ," + " Discount = " + discount + " , TotalPrice = " + totalPrice + "Where id = " + id;
            DBUtilities.Instance.ExecuteNonQuery(SQL);
        }

        public int GetUncheckBillIDByTableID(int id)
        {
            string SQL = "Select * From dbo.Bill Where IDTable = " + id + " And Status = 0";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);
            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }


        public void InsertBill(int id)
        {
            string SQL = "EXEC USP_InsertBill @idtable ";
            DBUtilities.Instance.ExecuteNonQuery(SQL, new object[] { id });
        }

        public int GetMaxBillID()
        {
            string SQL = "Select MAX(ID) From dbo.Bill";
            try
            {
                return (int)DBUtilities.Instance.ExecuteScalar(SQL);
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            string SQL = "EXEC ASP_GetListBillByDate @checkIn , @checkOut ";
            return DBUtilities.Instance.ExecuteQuery(SQL, new object[] { checkIn, checkOut});
        }

        public void DeleteBillByTableID(int idTable)
        {
            int result = -1;
            string SQL = string.Format("Delete dbo.Bill Where IDTable = {0} And Status = 0 ", idTable);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
        }
    }
}
