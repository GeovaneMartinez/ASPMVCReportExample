using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingAlgo
{
    public class TradeResults
    {
        public string Date { get; set; }
        public string EndDate { get; set; }
        public string Strategy { get; set; }
        public double StartingPrice { get; set; }
        public double EndingPrice { get; set; }
        public int Shares { get; set; }
        public double Percentage { get; set; }
        public double ProfitLoss { get; set; }

    }
}