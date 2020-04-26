using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }

        public List<MenuDTO> GetListMenuByTableId(int id)
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string SQL = "Select f.Name, bi.Count, f.Price, f.Price * bi.Count AS TotalPrice "
                       + "FROM dbo.BillInFo AS bi, dbo.Bill AS b, dbo.Food AS f "
                       + "Where b.ID = bi.IDBill AND f.ID = bi.IDFood AND b.Status = 0 AND b.IDTable = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }

    }
}
