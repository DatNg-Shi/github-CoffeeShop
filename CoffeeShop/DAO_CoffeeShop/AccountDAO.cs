using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }

        private AccountDAO() { }

        public bool CheckLogin(string username, string password)
        {
            DataTable result;
            string SQL = "USP_Login @userName , @passWord";
            result = DBUtilities.Instance.ExecuteQuery(SQL, new object[] { username, password});
             
            return result.Rows.Count > 0;
        }

        public AccountDTO GetAccountByUsername(string username)
        {
            string SQL = "Select * From Account Where UserName = '" + username + "' ";
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            foreach (DataRow item in data.Rows)
            {
                return new AccountDTO(item);
            }
            return null;
        }

        public bool UpdateProfile(string username, string displayname, string password, string newpass)
        {
            string SQL = "EXEC USP_UpdateAccount @username , @displayname , @password , @newpassword ";
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL, new object[] { username, displayname, password, newpass});
            return result > 0;
        }

        public DataTable GetListAccount()
        {
            DataTable data = null;
            string SQL = "Select UserName, DisplayName AS N'FullName', Role From dbo.Account AS a, dbo.AccountType AS at Where a.Type = at.TypeID ";
            data = DBUtilities.Instance.ExecuteQuery(SQL);

            return data;
        }


        public bool AddAccount(string username, string displayname, int type)
        {
            string SQL = string.Format("Insert dbo.Account (UserName, DisplayName, Type) Values (N'{0}', N'{1}', {2}) ", username, displayname, type);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool UpdateAccount(string username, string displayname, int type)
        {
            string SQL = string.Format("Update dbo.Account Set UserName = N'{0}', DisplayName = N'{1}', Type = {2} Where UserName = N'{3}' ", username, displayname, type, username);
            int result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }

        public bool DeleteAccount(string username)
        {
            int result = -1;
            string SQL = string.Format("Delete dbo.Account Where UserName = N'{0}' ", username);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);
            return result > 0;

        }

        public bool IsExisedtAccount(string username)
        {
            string SQL = string.Format("Select UserName From dbo.Account Where UserName = N'{0}' ", username);
            DataTable data = DBUtilities.Instance.ExecuteQuery(SQL);

            return data.Rows.Count > 0;
        }

        public bool ResetPassword(string username)
        {
            int result = -1;
            string SQL = string.Format("Update dbo.Account Set Password = 1 Where UserName = N'{0}' ", username);
            result = DBUtilities.Instance.ExecuteNonQuery(SQL);

            return result > 0;
        }
    }
}
