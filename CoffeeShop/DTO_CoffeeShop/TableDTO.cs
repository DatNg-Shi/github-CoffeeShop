using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class TableDTO
    {
        private int id;
        private string name;
        private string status;

        public TableDTO(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public TableDTO (DataRow row)
        {
            Id = (int)row["ID"];
            Name = row["Name"].ToString();
            Status = row["Status"].ToString();
            
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
