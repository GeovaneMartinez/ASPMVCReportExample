using ReportingAlgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class RiskController : Controller
    {

        AlpacaEntities dbcontext = new AlpacaEntities();

        public ActionResult Index()
        {
            List<Allocations> allocations = dbcontext.Allocations.ToList();
            AllocationsViewModel theAllocations = new AllocationsViewModel();
            theAllocations.allocations = allocations;

            List<RiskAmount> riskAmountList = new List<RiskAmount>();

            foreach (var item in theAllocations.allocations)
            {
                RiskAmount riskAmount = new RiskAmount();
                riskAmount.Strategy = item.Strategy;

                Double strAmount;
                Double.TryParse(item.Amount, out strAmount);
                riskAmount.Amount = strAmount;

                double riskPercentageAmount = 0;
                double targetPercentage = 0;
                if (item.Strategy.Equals("ShortMorningSpike"))
                {
                    riskPercentageAmount = 0.018;
                    targetPercentage = 0.0425;
                }
                else if (item.Strategy.Equals("TenAmSpike"))
                {
                    riskPercentageAmount = 0.014;
                    targetPercentage = 0.045;
                }
                else if (item.Strategy.Equals("Breakdown"))
                {
                    riskPercentageAmount = 0.025;
                    targetPercentage = 0.031;
                }
                else if (item.Strategy.Equals("Breakout"))
                {
                    riskPercentageAmount = 0.031;
                    targetPercentage = 0.02;
                }
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    riskPercentageAmount = 0.02;
                    targetPercentage = 0.02;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
                {
                    riskPercentageAmount = 0.03;
                    targetPercentage = 0.036;
                }
                else if (item.Strategy.Equals("jnugShort"))
                {
                    riskPercentageAmount = 0.024;
                    targetPercentage = 0.03;
                }
                else if (item.Strategy.Equals("gushShortTwoPercent"))
                {
                    riskPercentageAmount = 0.04;
                    targetPercentage = 0.05;
                }

                riskAmount.RiskPercentage = riskPercentageAmount;
                riskAmount.TargetPercentage = targetPercentage;

                riskAmount.RiskDollarAmount = CalculateRiskDollarAmount(riskPercentageAmount, strAmount);
                riskAmount.TargetAmount = CalculateTargetAmount(targetPercentage, strAmount);

                riskAmountList.Add(riskAmount);
            }



            return View(riskAmountList);
        }

        public double CalculateRiskDollarAmount(double riskPerentageAmount, double strAmount)
        {
            double amountAtRisk = 0;
            amountAtRisk = riskPerentageAmount * strAmount;

            return amountAtRisk;
        }

        public double CalculateTargetAmount(double targetPercentage, double strAmount)
        {
            double targetAmount = 0;
            targetAmount = targetPercentage * strAmount;

            return targetAmount;
        }

    }
}