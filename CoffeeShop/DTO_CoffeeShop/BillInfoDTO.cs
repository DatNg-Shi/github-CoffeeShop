using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class BillInfoDTO
    {
        private int id;
        private int idBill;
        private int idFood;
        private int count;

        public BillInfoDTO(int id, int idBill, int idFood, int count)
        {
            Id = id;
            IdBill = idBill;
            IdFood = idFood;
            Count = count;
        }

        public BillInfoDTO(DataRow row)
        {
            Id = (int)row["ID"];
            IdBill = (int)row["IDBill"];
            IdFood = (int)row["IDFood"];
            Count = (int)row["Count"];

        }

        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
