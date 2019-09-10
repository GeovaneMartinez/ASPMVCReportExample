using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportingAlgo.Models;

namespace ReportingAlgo.Controllers
{
    public class ActualController : Controller
    {
        AlpacaEntities dbcontext = new AlpacaEntities();
        string _username = "xxxxxxxx";

        public ActionResult Index()
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
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                }
                else if (item.Strategy.Equals("jnugBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                }
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
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
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    tradeResult.EndingPrice = item.EndingLABDPrice;
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

        //[Authorize(Roles = "Administrator")]
        public ActionResult EditTransactionsPage()
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {
                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            List<ActualTransactionDetails> transactionsDetails = new List<ActualTransactionDetails>();

            var transactions = dbcontext.ActualTransactions;

            foreach (var item in transactions)
            {
                ActualTransactionDetails details = new ActualTransactionDetails();
                details.ID = item.ID;
                details.Date = item.Date.Substring(0, 10);
                details.StartingLABUPrice = item.StartingLABUPrice;               
                details.StartingLABDPrice = item.StartingLABDPrice;

                details.Strategy = item.Strategy;
                details.StartingType = item.StartingType;


                string myShares = item.Shares.ToString();
                Int32 NumOfShares;
                Int32.TryParse(myShares, out NumOfShares);
                details.Shares = NumOfShares;
                
                details.EndingLABUPrice = item.EndingLABUPrice;
                details.EndingLABDPrice = item.EndingLABDPrice;

                details.Type = item.Type;
                details.Reason = item.Reason;

                details.ActualStartingLABUPrice = item.ActualLABUStartingPrice;
                details.ActualStartingLABDPrice = item.ActualLABDStartingPrice;

                details.ActualEndingLABUPrice = item.ActualLABUEndingPrice;
                details.ActualEndingLABDPrice = item.ActualLABDEndingPrice;

                transactionsDetails.Add(details);
            }

            List<EditTradeResultsView> tradeResults = new List<EditTradeResultsView>();

            foreach (var item in transactionsDetails)
            {
                EditTradeResultsView tradeResult = new EditTradeResultsView();
                tradeResult.ID = item.ID;
                tradeResult.Date = item.Date;
                tradeResult.Strategy = item.Strategy;
                tradeResult.Shares = item.Shares;
                double startDifference = 0;
                double endDifference = 0;

                if (item.Strategy.Equals("ShortMorningSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;
                    
                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);


                }
                else if (item.Strategy.Equals("NineFortyFiveSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("TenAmSpike"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("Breakdown"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("Breakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABUPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABUPrice;
                    tradeResult.EndingPrice = item.EndingLABUPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("ShortBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("jnugBreakout"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABUPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABUPrice;
                    tradeResult.EndingPrice = item.EndingLABUPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("jnugShort"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("GushShortTwoPercent"))
                {
                    tradeResult.StartingPrice = item.StartingLABDPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABDPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABDPrice;
                    tradeResult.EndingPrice = item.EndingLABDPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                else if (item.Strategy.Equals("GapDownReversal"))
                {
                    tradeResult.StartingPrice = item.StartingLABUPrice;
                    tradeResult.ActualStartingPrice = item.ActualStartingLABUPrice;
                    tradeResult.ActualEndingPrice = item.ActualEndingLABUPrice;
                    tradeResult.EndingPrice = item.EndingLABUPrice;

                    startDifference = GetStartPriceDifference(tradeResult.StartingPrice, tradeResult.ActualStartingPrice);
                    endDifference = GetEndPriceDifference(tradeResult.EndingPrice, tradeResult.ActualEndingPrice);

                    tradeResult.DifferencePerShare = Math.Round(startDifference + endDifference, 2);
                    tradeResult.DifferenceTotal = Math.Round(tradeResult.DifferencePerShare * (double)tradeResult.Shares, 2);
                }
                


                //double profitLossPerShare = tradeResult.EndingPrice - tradeResult.StartingPrice;

                //tradeResult.ProfitLoss = Math.Round((tradeResult.EndingPrice - tradeResult.StartingPrice) * tradeResult.Shares, 2);

                //total += tradeResult.ProfitLoss;
                //tradeResult.Percentage = Math.Round((profitLossPerShare / tradeResult.StartingPrice) * 100, 2);

                tradeResults.Add(tradeResult);

            }

            //ViewBag.Total = total;


            //int k = 1;
            //String jsonChartData = "[0, 0], ";
            //double runningTotal = 0;

            //foreach (var item in tradeResults)
            //{
            //    runningTotal = runningTotal + item.ProfitLoss;
            //    jsonChartData += "[" + k + ", " + runningTotal + "]";

            //    if ((k) != tradeResults.Count)
            //    {
            //        jsonChartData += ", ";
            //    }
            //    k++;
            //}

            //ViewBag.JsonChartData = jsonChartData;

            return View(tradeResults);
        }


        protected double GetStartPriceDifference(string startingPrice, string actualStartingPrice)
        {
            Double startPrice;
            Double.TryParse(startingPrice, out startPrice);

            Double actualStartPrice;
            Double.TryParse(actualStartingPrice, out actualStartPrice);
            
            return startPrice - actualStartPrice;
        }

        protected double GetEndPriceDifference(string endingPrice, string actualEndingPrice)
        {
            Double endPrice;
            Double.TryParse(endingPrice, out endPrice);

            Double actualEndPrice;
            Double.TryParse(actualEndingPrice, out actualEndPrice);

            return actualEndPrice - endPrice;
        }


        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public ActionResult EditPrice(int Id)
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            ActualTransactions theTransaction =  dbcontext.ActualTransactions.Find(Id);
            ViewBag.TheStrategy = theTransaction.Strategy;

            return View(theTransaction);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public ActionResult EditPrice(ActualTransactions actualTransaction)
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            ActualTransactions transactionToSave = dbcontext.ActualTransactions.Find(actualTransaction.ID);
            transactionToSave.ActualLABUStartingPrice = actualTransaction.ActualLABUStartingPrice;
            transactionToSave.ActualLABUEndingPrice = actualTransaction.ActualLABUEndingPrice;
            transactionToSave.ActualLABDStartingPrice = actualTransaction.ActualLABDStartingPrice;
            transactionToSave.ActualLABDEndingPrice = actualTransaction.ActualLABDEndingPrice;

            dbcontext.SaveChanges();
            return RedirectToAction("EditTransactionsPage"); ;
        }


        [HttpGet]
        public ActionResult EditLABUPrice()
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            LABU labuTimestamp = dbcontext.LABU.OrderByDescending(t => t.timestamp).Take(1).FirstOrDefault();
            return View(labuTimestamp);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public ActionResult EditLABUPrice(LABU labu)
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            LABU editedLabu = dbcontext.LABU.Find(labu.ID);
            editedLabu.close = labu.close;

            dbcontext.SaveChanges();
            return RedirectToAction("Index", "Home"); ;
        }

        [HttpGet]
        public ActionResult EditLABDPrice()
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            LABD labuTimestamp = dbcontext.LABD.OrderByDescending(t => t.timestamp).Take(1).FirstOrDefault();


            return View(labuTimestamp);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public ActionResult EditLABDPrice(LABD labd)
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            LABD editedLabd = dbcontext.LABD.Find(labd.ID);
            editedLabd.close = labd.close;

            dbcontext.SaveChanges();
            return RedirectToAction("Index", "Home"); ;
        }


        [HttpGet]
        public ActionResult Allocations()
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            List<Allocations> allocations = dbcontext.Allocations.ToList();
            AllocationsViewModel theAllocations = new AllocationsViewModel();
            theAllocations.allocations = allocations;


            return View(theAllocations);
        }


        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public ActionResult Allocations(AllocationsViewModel allocationsViewModel)
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            List<Allocations> originalAllocations = dbcontext.Allocations.ToList();

            foreach (var item in originalAllocations)
            {
                foreach (var allocation in allocationsViewModel.allocations)
                {
                    if(item.Strategy.Equals(allocation.Strategy))
                    {
                        if(!item.Amount.Equals(allocation.Amount))
                        {
                            item.Amount = allocation.Amount;
                            dbcontext.SaveChanges();
                        }
                        if(item.IsActive != allocation.IsActive)
                        {
                            item.IsActive = allocation.IsActive;
                            dbcontext.SaveChanges();
                        }
                    }
                }
            }

            return RedirectToAction("EditTransactionsPage", "Actual"); ;
        }




        [HttpGet]
        public ActionResult InsertClosePosition()
        {
            if (Session["userId"] != null && Session["userId"].Equals(_username))
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            


            return View();
        }



    }
}
