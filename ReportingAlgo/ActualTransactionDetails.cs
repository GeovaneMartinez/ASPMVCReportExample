using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingAlgo
{
    public class ActualTransactionDetails
    {
        public int ID{ get; set; }
        public string Date { get; set; }
        public string StartingLABUPrice { get; set; }
        public string StartingLABDPrice { get; set; }
        public string Strategy { get; set; }
        public string StartingType { get; set; }
        public int? Shares { get; set; }
        public string EndingLABUPrice { get; set; }
        public string EndingLABDPrice { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string ActualStartingLABUPrice { get; set; }
        public string ActualStartingLABDPrice { get; set; }
        public string ActualEndingLABUPrice { get; set; }
        public string ActualEndingLABDPrice { get; set; }
        public string EndDate { get; set; }

    }
}