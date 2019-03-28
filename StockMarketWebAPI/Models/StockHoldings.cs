using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockMarketWebAPI.Models
{
    public class StockHoldings
    {
        public Int32 userId { get; set; }
        public Int32 stockId { get; set; }
        public Int32 quantity { get; set; }
    }
}