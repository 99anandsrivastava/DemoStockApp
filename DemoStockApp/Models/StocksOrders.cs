using System;
using System.Collections.Generic;

namespace DemoStockApp.Models
{
    public partial class StocksOrders
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string StockName { get; set; }
        public double StockCurrentPrice { get; set; }
        public double StockPurchasePrice { get; set; }
        public int QuantityPurchased { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
