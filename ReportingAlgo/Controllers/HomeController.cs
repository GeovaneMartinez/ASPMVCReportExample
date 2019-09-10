using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class HomeController : Controller
    {

        AlpacaEntities dbcontext = new AlpacaEntities();

        public ActionResult Index()
        {
            LABU previousDayTimestampAlgoExampleLABU = dbcontext.LABU.OrderByDescending(t => t.timestamp).Take(1).FirstOrDefault();
            string lastUpdatedAlgoExampleLABU = previousDayTimestampAlgoExampleLABU.timestamp;

            LABD previousDayTimestampAlgoExampleLABD = dbcontext.LABD.OrderByDescending(t => t.timestamp).Take(1).FirstOrDefault();
            string lastUpdatedAlgoExampleLABD = previousDayTimestampAlgoExampleLABD.timestamp;

            JNUG previousDayTimestampAlgoExampleJNUG = dbcontext.JNUG.OrderByDescending(t => t.timestamp).Take(1).FirstOrDefault();
            string lastUpdatedAlgoExampleJNUG = previousDayTimestampAlgoExampleJNUG.timestamp;

            ViewBag.AlgoExampleLABUClosePrice = previousDayTimestampAlgoExampleLABU.close.ToString();
            ViewBag.AlgoExampleLABDClosePrice = previousDayTimestampAlgoExampleLABD.close.ToString();
            ViewBag.AlgoExampleJNUGClosePrice = previousDayTimestampAlgoExampleJNUG.close.ToString();
            ViewBag.lastUpdatedAlgoExampleLABU = lastUpdatedAlgoExampleLABU;
            ViewBag.lastUpdatedAlgoExampleLABD = lastUpdatedAlgoExampleLABD;
            ViewBag.lastUpdatedAlgoExampleJNUG = lastUpdatedAlgoExampleJNUG;




            string LABUTimeStamp = "";
            using (var context = new AlpacaEntities())
            {
                var HistoricalDBLABUTimeStamp = context.Database.SqlQuery<string>(
                                   "SELECT top 1 timestamp FROM [Historical_Data].[dbo].[LABU] order by ID desc").ToList();

                LABUTimeStamp = HistoricalDBLABUTimeStamp[0];
            }

            ViewBag.HistoricalDBLABUTimeStamp = LABUTimeStamp;

            string LABDTimeStamp = "";
            using (var context = new AlpacaEntities())
            {
                var HistoricalDBLABDTimeStamp = context.Database.SqlQuery<string>(
                                   "SELECT top 1 timestamp FROM [Historical_Data].[dbo].[LABD] order by ID desc").ToList();

                LABDTimeStamp = HistoricalDBLABDTimeStamp[0];
            }

            ViewBag.HistoricalDBLABDTimeStamp = LABDTimeStamp;

            string JNUGTimeStamp = "";
            using (var context = new AlpacaEntities())
            {
                var HistoricalDBJNUGTimeStamp = context.Database.SqlQuery<string>(
                                   "SELECT top 1 timestamp FROM [Historical_Data].[dbo].[JNUG] order by ID desc").ToList();

                JNUGTimeStamp = HistoricalDBJNUGTimeStamp[0];
            }

            ViewBag.HistoricalDBJNUGTimeStamp = JNUGTimeStamp;

            string AlgoExampleLABUUpdated = "false";
            string AlgoExampleLABDUpdated = "false";
            string AlgoExampleJNUGUpdated = "false";
            string HistoricalDBLabuUpdated = "false";
            string HistoricalDBLabdUpdated = "false";
            string HistoricalDBJnugUpdated = "false";

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string HistoricalDBlabuTimeStamp = LABUTimeStamp;
            string HistoricalDBLabdTimeStamp = LABDTimeStamp;
            string HistoricalDBJnugTimeStamp = JNUGTimeStamp;
            string AlgoExampleLABUTimeStamp = lastUpdatedAlgoExampleLABU;
            string AlgoExampleLABDTimeStamp = lastUpdatedAlgoExampleLABD;
            string AlgoExampleJNUGTimeStamp = lastUpdatedAlgoExampleJNUG;

            if (AlgoExampleLABUTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                AlgoExampleLABUUpdated = "true";
            }
            if (AlgoExampleLABDTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                AlgoExampleLABDUpdated = "true";
            }
            if (AlgoExampleJNUGTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                AlgoExampleJNUGUpdated = "true";
            }
            if (HistoricalDBlabuTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                HistoricalDBLabuUpdated = "true";
            }
            if (HistoricalDBLabdTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                HistoricalDBLabdUpdated = "true";
            }
            if (HistoricalDBJnugTimeStamp.Equals(currentDate + " 16:00:00"))
            {
                HistoricalDBJnugUpdated = "true";
            }

            ViewBag.AlgoExampleLABUUpdated = AlgoExampleLABUUpdated;
            ViewBag.AlgoExampleLABDUpdated = AlgoExampleLABDUpdated;
            ViewBag.AlgoExampleJNUGUpdated = AlgoExampleJNUGUpdated;
            ViewBag.HistoricalDBLabuUpdated = HistoricalDBLabuUpdated;
            ViewBag.HistoricalDBLabdUpdated = HistoricalDBLabdUpdated;
            ViewBag.HistoricalDBJnugUpdated = HistoricalDBJnugUpdated;

            //List<Allocations> allocations = dbcontext.Allocations.ToList();
            //ViewBag.Allocations = allocations;

            return View();
        }

        public ActionResult Transactions()
        {
            List<TransactionDetails> transactionsDetails = new List<TransactionDetails>();

            var transactions = dbcontext.GetTrades();

            foreach (var item in transactions)
            {
                TransactionDetails details = new TransactionDetails();
                details.Date = item.Date;

                Double StartLABU;
                Double.TryParse(item.StartingLABUPrice, out StartLABU);
                details.StartingLABUPrice = StartLABU;

                Double StartLABD;
                Double.TryParse(item.StartingLABDPrice, out StartLABD);
                details.StartingLABDPrice = StartLABD;

                details.Strategy = item.Strategy;
                details.StartingType = item.StartingType;


                string myShares = item.Shares.ToString(); 
                Int32 NumOfShares;
                Int32.TryParse(myShares, out NumOfShares);
                details.Shares = NumOfShares;

                Double EndLABU;
                Double.TryParse(item.EndingLABUPrice, out EndLABU);
                details.EndingLABUPrice = EndLABU;

                Double EndLABD;
                Double.TryParse(item.EndingLABDPrice, out EndLABD);
                details.EndingLABDPrice = EndLABD;

                details.Type = item.Type;
                details.Reason = item.Reason;

                transactionsDetails.Add(details);
            }

            List<TradeResults> tradeResults = new List<TradeResults>();
            double total = 0;

            foreach (var item in transactionsDetails)
            {
                TradeResults tradeResult = new TradeResults();
                tradeResult.Date = item.Date;
                tradeResult.Strategy = item.Strategy;
                tradeResult.Shares = item.Shares;
                
                if(item.Strategy.Equals("ShortMorningSpike"))
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
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
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
                else if (item.Strategy.Equals("ShortBreakout"))
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
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.EndingPrice = item.EndingLABUPrice;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
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
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                }


                double profitLossPerShare = tradeResult.EndingPrice - tradeResult.StartingPrice;

                tradeResult.ProfitLoss = Math.Round((tradeResult.EndingPrice - tradeResult.StartingPrice) * tradeResult.Shares, 2);
                
                total += tradeResult.ProfitLoss;
                tradeResult.Percentage = Math.Round((profitLossPerShare / tradeResult.StartingPrice) * 100, 2);

                tradeResults.Add(tradeResult);

            }

            ViewBag.Total = total;


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


        public ActionResult Log()
        {
            List<Log> logs = dbcontext.Log.OrderByDescending(x => x.ID).Take(850).ToList();

            return View(logs);
        }


        public ActionResult Testing()
        {


            return View();
        }


        public ActionResult jsonTest()
        {

            string jsonMessage = "Inserted some trades test.";
           

            return Json(new { success = jsonMessage }, JsonRequestBehavior.AllowGet);
        }



    }
}