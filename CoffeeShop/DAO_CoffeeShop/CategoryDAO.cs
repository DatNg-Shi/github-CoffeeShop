using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<CategoryDTO> GetListCategory()
        {
            List<CategoryDTO> listCategory = new List<CategoryDTO>();
            string SQL = "Select * From FoodCategory";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                CategoryDTO category = new CategoryDTO(item);
                listCategory.Add(category);
            }

            return listCategory;
        }

        public CategoryDTO GetCategoryByID(int id)
        {
            CategoryDTO category = null;
            string SQL = "Select * From FoodCategory Where ID = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                category = new CategoryDTO(item);
                return category;
            }

            return category;
        }


        public bool AddCategory(string name)
        {
            string SQL = string.Format("Insert dbo.FoodCategory (Name) Values (N'{0}') ", name);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool UpdateCategory(string name, int idCate)
        {
            string SQL = string.Format("Update dbo.FoodCategory Set Name = N'{0}' Where ID = {1} ", name, idCate);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool DeleteCategory(int idCate)
        {
            int result = -1;
            FoodDAO.Instance.DeleteFoodByIdCategory(idCate);

            string SQL = string.Format("Delete dbo.FoodCategory Where ID = {0} ", idCate);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
            return result > 0;

        }


        public bool IsExistedCategory(string name)
        {
            string SQL = "Select Name From dbo.FoodCategory Where Name = '" + name + "'";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            return data.Rows.Count > 0;
        }
    }
}
