using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockMarketWebAPI.Models
{
    public class Stock
    {
        public Int32 stockId { get; set; }
        public string stockName { get; set; }
        public decimal stockValue { get; set; }
        public decimal stockTrend { get; set; }
    }
}