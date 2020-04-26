using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class BillInfoDAO
    {
        
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfoDTO> GetListBillInfo(int id)
        {
            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();
            string SQL = "Select * From dbo.BillInfo Where IDBill = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                BillInfoDTO billInfo = new BillInfoDTO(item);
                listBillInfo.Add(billInfo);
            }

            return listBillInfo;
        }

        public void InsertBillInfno(int idBill, int idFood, int count)
        {
            string SQL = "EXEC USP_InsertBillInfo @idBill , @idFood , @count";
            DBUtilities.Instance.ExecuteNonQuery(SQL, new object[] { idBill, idFood, count });
        }

        public bool DeleteBillInfoByFoodID(int id)
        {
            int result = -1;
            string SQL = "Delete dbo.BillInfo Where IDFood = " + id;
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public void DeleteBillInfoByBillID(int idBill)
        {
            int result = -1;
            string SQL = "Delete dbo.BillInfo Where IDBill = " + idBill;
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
  
        }

    }
}
