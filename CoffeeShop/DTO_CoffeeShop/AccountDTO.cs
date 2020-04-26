using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class AccountDTO
    {
        private string username;
        private string displayName;
        private string password;
        private int type;

        public AccountDTO(string username, string displayName, int type, string password = null)
        {
            Username = username;
            DisplayName = displayName;
            Password = password;
            Type = type;
        }

        public AccountDTO(DataRow row)
        {
            Username = row["UserName"].ToString();
            DisplayName = row["DisplayName"].ToString();
            Type = (int)row["Type"];
            Password = row["Password"].ToString();
        }

        public string Username { get => username; set => username = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }
    }
}
