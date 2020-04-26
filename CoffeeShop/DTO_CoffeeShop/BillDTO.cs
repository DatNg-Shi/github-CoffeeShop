using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CoffeeShop
{
    public class BillDTO
    {
        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        int status;
        int discount;

        public BillDTO(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount = 0)
        {
            Id = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            Status = status;
            Discount = discount;
        }

        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public BillDTO(DataRow row)
        {
            Id = (int)row["ID"];
            DateCheckIn = (DateTime?)row["DateCheckIn"];
            var dateCheckOutTemp = row["DateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
            Status = (int)row["Status"];

            if (row["Discount"].ToString() != "")
            {
                Discount = (int)row["Discount"];
            }
        }



    }
}
