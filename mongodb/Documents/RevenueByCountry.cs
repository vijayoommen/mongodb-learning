using System;
using System.Collections.Generic;
using System.Text;

namespace mongodb.Documents
{
    public class RevenueByCountry
    {
        public string Country { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
