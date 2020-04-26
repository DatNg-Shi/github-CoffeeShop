using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }


        private TableDAO() { }

        public List<TableDTO> LoadTableList()
        {
            List<TableDTO> tableList = new List<TableDTO>();

            DataTable data = DBUtilities.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public List<TableDTO> LoadTableListDeleted()
        {
            List<TableDTO> tableList = new List<TableDTO>();
            string SQL = "SELECT ID, Name, Status FROM dbo.TableFood WHERE Disable = N'yes'";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public TableDTO GetStatusByTableID(int id)
        {
            TableDTO table = null;
            string SQL = "Select * From dbo.TableFood Where ID = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                table = new TableDTO(item);
                return table;
            }

            return table;
        }


        public void SwitchTable(int idTable1, int idTable2)
        {
            string SQL = "USP_SwitchTable @idTable1 , @idTable2";
            DBUtilities.Instance.ExecuteQuery(SQL, new object[] { idTable1, idTable2 });
        }


        public bool AddTable(string name)
        {
            string SQL = string.Format("Insert dbo.TableFood (Name, Status) Values (N'{0}', N'Empty') ", name);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool UpdateTable(string name, string status, int idTable)
        {
            string SQL = string.Format("Update dbo.TableFood Set Name = N'{0}' , Status = N'{1}' Where ID = {2} ", name, status, idTable);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
            int result = -1;

            string SQL = string.Format("Update dbo.TableFood Set Disable = N'yes', Status = N'Empty' Where ID = {0} ", idTable);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool IsExistedTable(string name)
        {
            string SQL = "Select Name From dbo.TableFood Where Name = '" + name + "'";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            return data.Rows.Count > 0;
        }

        public bool RestoreTable(int idTable)
        {
            int result = -1;

            string SQL = string.Format("Update dbo.TableFood Set Disable = N'no' Where ID = {0} ", idTable);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }
    }
}
