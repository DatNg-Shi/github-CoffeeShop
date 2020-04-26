using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class AccountTypeDAO
    {
        private static AccountTypeDAO instance;

        public static AccountTypeDAO Instance
        {
            get { if (instance == null) instance = new AccountTypeDAO(); return AccountTypeDAO.instance; }
            private set { AccountTypeDAO.instance = value; }
        }

        private AccountTypeDAO() { }

        public List<AccountTypeDTO> GetListType()
        {
            List<AccountTypeDTO> listType = new List<AccountTypeDTO>();
            string SQL = "Select * From AccountType";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                AccountTypeDTO type = new AccountTypeDTO(item);
                listType.Add(type);
            }

            return listType;
        }

        public AccountTypeDTO GetTypeByID(int id)
        {
            AccountTypeDTO type = null;
            string SQL = "Select * From AccountType Where TypeID = " + id;
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                type = new AccountTypeDTO(item);
                return type;
            }

            return type;
        }
    }
}
