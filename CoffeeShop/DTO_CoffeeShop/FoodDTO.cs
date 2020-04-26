using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class FoodDTO
    {
        private int idFood;
        private string name;
        private int idCate;
        private double price;

        public FoodDTO(int idFood, string name, int idCate, double price)
        {
            IdFood = idFood;
            Name = name;
            IdCate = idCate;
            Price = price;
        }

        public FoodDTO(DataRow row)
        {
            IdFood = (int)row["ID"];
            Name = row["Name"].ToString();
            IdCate = (int)row["IDCategory"];
            Price = (double)row["Price"];
        }

        public int IdFood { get => idFood; set => idFood = value; }
        public string Name { get => name; set => name = value; }
        public int IdCate { get => idCate; set => idCate = value; }
        public double Price { get => price; set => price = value; }
    }
}
