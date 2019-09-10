using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingAlgo
{
    public class EditTradeResultsView
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Strategy { get; set; }
        public string StartingPrice { get; set; }
        public string EndingPrice { get; set; }
        public string ActualStartingPrice { get; set; }
        public string ActualEndingPrice { get; set; }
        public int? Shares { get; set; }
        public double DifferencePerShare { get; set; }
        public double DifferenceTotal { get; set; }

    }
}