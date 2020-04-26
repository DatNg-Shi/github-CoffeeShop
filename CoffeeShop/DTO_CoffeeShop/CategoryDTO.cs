using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class CategoryDTO
    {
        private int id;
        private string name;

        public CategoryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryDTO(DataRow row)
        {
            Id = (int)row["ID"];
            Name = row["Name"].ToString();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
