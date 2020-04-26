using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class MenuDTO
    {
        private string foodName;
        private int count;
        private double price;
        private double totalPrice;

        public MenuDTO(string foodName, int count, double price, double totalPrice = 0)
        {
            FoodName = foodName;
            Count = count;
            Price = price;
            TotalPrice = totalPrice;
        }
        public MenuDTO(DataRow row)
        {
            FoodName = row["Name"].ToString();
            Count = (int)row["Count"];
            Price = (double)row["Price"];
            TotalPrice = (double)row["TotalPrice"];
        }

        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public double Price { get => price; set => price = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
