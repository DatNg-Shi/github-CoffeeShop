using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<FoodDTO> GetFoodByCategoryID(int id)
        {
            List<FoodDTO> listFood = new List<FoodDTO>();
            string SQL = "Select * From Food Where IDCategory = " + id + " And StatusFood = 1";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                FoodDTO food = new FoodDTO(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public DataTable GetListFood()
        {
            string SQL = "Select f.Name, fc.Name as N'Category', Price, f. ID From dbo.Food as f, dbo.FoodCategory as fc Where f.IDCategory = fc.ID ";
            DataTable dtFood = DBUtilities.Instance.ExecuteQuery(SQL);
            return dtFood;
        }

        public DataTable GetListFoodActiveOrInactive(int check)
        {
            string SQL = "Select f.Name, fc.Name as N'Category', Price, f. ID From dbo.Food as f, dbo.FoodCategory as fc Where f.IDCategory = fc.ID And f.StatusFood = @checked";
            DataTable dtFood = DBUtilities.Instance.ExecuteQuery(SQL, new object[] { check });
            return dtFood;
        }

        public FoodDTO GetCategoryIDByFoodID(int id)
        {
            FoodDTO food = null;
            string SQL = "Select * From Food Where ID = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                food = new FoodDTO(item);
                return food;
            }

            return food;
        }

        public bool AddFood(string name, int idCate, float price)
        {
            string SQL = string.Format("Insert dbo.Food (Name, IDCategory, Price) Values (N'{0}', {1}, {2}) ", name, idCate, price);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool UpdateFood(int idFood, string name, int idCate, float price)
        {
            string SQL = string.Format("Update dbo.Food Set Name = N'{0}', IDCategory = {1}, Price = {2} Where ID = {3} ", name, idCate, price, idFood);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            int result = -1;
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string SQL = string.Format("Update dbo.Food Set StatusFood = 0 Where ID = {0} ", idFood);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
            return result > 0;
        }

        public bool RestoreFood(int idFood)
        {
            int result = -1;
            string SQL = string.Format("Update dbo.Food Set StatusFood = 1 Where ID = {0} ", idFood);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
            return result > 0;


        }

        public DataTable SearchFoodByName(string name, string check)
        {
            string SQL = "Select f.Name, fc.Name as N'Category', Price, f. ID From dbo.Food as f, dbo.FoodCategory as fc Where f.IDCategory = fc.ID And f.Name like '%" + name + "%' And f.StatusFood like '%" + check + "%' ";
            DataTable dtFood = DBUtilities.Instance.ExecuteQuery(SQL);
            return dtFood;
        }

        public bool IsExisedtFood(string name)
        {
            string SQL = string.Format("Select Name From dbo.Food Where Name = N'{0}' ", name);
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            return data.Rows.Count > 0;
        }

        public bool DeleteFoodByIdCategory(int idCate)
        {
            int result = -1;
            List<FoodDTO> listFood = GetFoodByCategoryID(idCate);
            if (listFood != null)
            {
                foreach (FoodDTO food in listFood)
                {
                    BillInfoDAO.Instance.DeleteBillInfoByFoodID(food.IdFood);
                }
            }
            string SQL = "Delete dbo.Food Where IDCategory = " + idCate;
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }
    }
}
