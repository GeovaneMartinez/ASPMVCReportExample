using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingAlgo
{
    public class TransactionDetails
    {
        public string Date { get; set; }
        public string EndDate { get; set; }
        public double StartingLABUPrice { get; set; }
        public double StartingLABDPrice { get; set; }
        public string Strategy { get; set; }
        public string StartingType { get; set; }
        public int Shares { get; set; }
        public double EndingLABUPrice { get; set; }
        public double EndingLABDPrice { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
    }
}