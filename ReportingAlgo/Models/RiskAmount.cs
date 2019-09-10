using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingAlgo.Models
{
    public class RiskAmount
    {
        public string Strategy { get; set; }
        public double Amount { get; set; }
        public double RiskPercentage { get; set; }
        public double RiskDollarAmount { get; set; }
        public double TargetPercentage { get; set; }
        public double TargetAmount { get; set; }
    }
}