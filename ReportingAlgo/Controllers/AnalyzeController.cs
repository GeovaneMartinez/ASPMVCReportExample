using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class AnalyzeController : Controller
    {
        AlpacaEntities dbcontext = new AlpacaEntities();
        // GET: Analyze
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Breakout()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if(item.Strategy.Equals("Breakout"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult Breakdown()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("Breakdown"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult TenAmSpike()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("TenAmSpike"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult ShortMorningSpike()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("ShortMorningSpike"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult JnugBreakout()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("jnugBreakout"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult JnugShort()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("jnugShort"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public ActionResult GushShortTwoPercent()
        {


            List<TradeResults> actualTransactions = GetActualTransactions();
            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in actualTransactions)
            {
                if (item.Strategy.Equals("GushShortTwoPercent"))
                {
                    tradeResults.Add(item);

                    total += item.ProfitLoss;
                    percentageTotal += item.Percentage;
                }
            }

            ViewBag.Total = total;
            ViewBag.PercentageTotal = percentageTotal;


            int k = 1;
            String jsonChartData = "[0, 0], ";
            double runningTotal = 0;

            foreach (var item in tradeResults)
            {
                runningTotal = runningTotal + item.ProfitLoss;
                jsonChartData += "[" + k + ", " + runningTotal + "]";

                if ((k) != tradeResults.Count)
                {
                    jsonChartData += ", ";
                }
                k++;
            }

            ViewBag.JsonChartData = jsonChartData;
            return View(tradeResults);
        }

        public List<TradeResults> GetActualTransactions()
        {
            List<TransactionDetails> transactionsDetails = new List<TransactionDetails>();

            var transactions = dbcontext.ActualTransactions;

            foreach (var item in transactions)
            {
                TransactionDetails details = new TransactionDetails();
                details.Date = item.Date;
                details.EndDate = item.EndDate;

                Double StartLABU;
                Double.TryParse(item.ActualLABUStartingPrice, out StartLABU);
                details.StartingLABUPrice = StartLABU;

                Double StartLABD;
                Double.TryParse(item.ActualLABDStartingPrice, out StartLABD);
                details.StartingLABDPrice = StartLABD;

                details.Strategy = item.Strategy;
                details.StartingType = item.StartingType;


                string myShares = item.Shares.ToString();
                Int32 NumOfShares;
                Int32.TryParse(myShares, out NumOfShares);
                details.Shares = NumOfShares;

                Double EndLABU;
                Double.TryParse(item.ActualLABUEndingPrice, out EndLABU);
                details.EndingLABUPrice = EndLABU;

                Double EndLABD;
                Double.TryParse(item.ActualLABDEndingPrice, out EndLABD);
                details.EndingLABDPrice = EndLABD;

                details.Type = item.Type;
                details.Reason = item.Reason;

                transactionsDetails.Add(details);
            }

            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;
            double percentageTotal = 0;

            foreach (var item in transactionsDetails)
            {
                TradeResults tradeResult = new TradeResults();
                tradeResult.Date = item.Date;
                tradeResult.EndDate = item.EndDate;
                tradeResult.Strategy = item.Strategy;
                tradeResult.Shares = item.Shares;

                if (item.Strategy.Equals("ShortMorningSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("NineFortyFiveSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("TenAmSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("Breakdown"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("Breakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugShort"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("GushShortTwoPercent"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }


                if (item.Strategy.Equals("ShortMorningSpike"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }
                else if (item.Strategy.Equals("NineFortyFiveSpike"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }
                else if (item.Strategy.Equals("TenAmSpike"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }
                else if (item.Strategy.Equals("Breakdown"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }
                else if (item.Strategy.Equals("Breakout"))
                {
                    tradeResult.EndingPrice = item.EndingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
                {
                    tradeResult.EndingPrice = item.EndingLABUPrice;
                }
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.EndingPrice = item.EndingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugShort"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }
                else if (item.Strategy.Equals("GushShortTwoPercent"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }

                double profitLossPerShare = tradeResult.EndingPrice - tradeResult.StartingPrice;

                tradeResult.ProfitLoss = Math.Round((tradeResult.EndingPrice - tradeResult.StartingPrice) * tradeResult.Shares, 2);

                total += tradeResult.ProfitLoss;
                tradeResult.Percentage = Math.Round((profitLossPerShare / tradeResult.StartingPrice) * 100, 2);
                percentageTotal += tradeResult.Percentage;

                tradeResults.Add(tradeResult);



            }



            return tradeResults;
        }





    }
}