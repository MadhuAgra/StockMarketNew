using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockMarketWebAPI.Models
{
    public class User
    {
        public Int32 userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public decimal cashValue { get; set; }
        public decimal stockValue { get; set; }

    }
}