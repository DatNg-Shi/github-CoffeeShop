using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class AccountTypeDTO
    {
        private int id;
        private string role;

        public AccountTypeDTO(int id, string role)
        {
            Id = id;
            Role = role;
        }

        public AccountTypeDTO(DataRow row)
        {
            Id = (int)row["TypeID"];
            Role = row["Role"].ToString();
        }

        public int Id { get => id; set => id = value; }
        public string Role { get => role; set => role = value; }
    }
}
