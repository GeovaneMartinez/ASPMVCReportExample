using ReportingAlgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class ReportController : Controller
    {

        AlpacaEntities dbcontext = new AlpacaEntities();
        // GET: Report
        [HttpGet]
        public ActionResult Monthly()
        {
            return View();
        }

        
        public ActionResult MonthlyDetails(String Month = "")
        {
            string todaysDate = "";
            if (String.IsNullOrWhiteSpace(Month))
            {
                todaysDate = DateTime.Now.ToString("MM/dd/yyyy");
                Month = todaysDate.Substring(0, 2);
            }

            string dateSelected = DateTime.Now.ToString("MM/dd/yyyy");
            string theMonthSelected = dateSelected.Substring(0, 2);
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
                if (item.Date.StartsWith(Month))
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
                    else if (item.Strategy.Equals("GushShortTwoPercent"))
                    {
                        tradeResult.StartingPrice = item.StartingLABDPrice;
                    }
                    else if (item.Strategy.Equals("jnugShort"))
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
                    else if (item.Strategy.Equals("GushShortTwoPercent"))
                    {
                        tradeResult.EndingPrice = item.EndingLABDPrice;
                    }
                    else if (item.Strategy.Equals("jnugShort"))
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

            }

            List<Month> monthList = new List<Month>();

            Month january = new Month();
            january.MonthValue = "01";
            january.MonthString = "January";
            monthList.Add(january);

            Month february = new Month();
            february.MonthValue = "02";
            february.MonthString = "February";
            monthList.Add(february);

            Month march = new Month();
            march.MonthValue = "03";
            march.MonthString = "March";
            monthList.Add(march);

            Month april = new Month();
            april.MonthValue = "04";
            april.MonthString = "April";
            monthList.Add(april);

            Month may = new Month();
            may.MonthValue = "05";
            may.MonthString = "May";
            monthList.Add(may);

            Month june = new Month();
            june.MonthValue = "06";
            june.MonthString = "June";
            monthList.Add(june);

            Month july = new Month();
            july.MonthValue = "07";
            july.MonthString = "July";
            monthList.Add(july);

            Month august = new Month();
            august.MonthValue = "08";
            august.MonthString = "August";
            monthList.Add(august);

            Month september = new Month();
            september.MonthValue = "09";
            september.MonthString = "September";
            monthList.Add(september);

            Month october = new Month();
            october.MonthValue = "10";
            october.MonthString = "October";
            monthList.Add(october);

            Month november = new Month();
            november.MonthValue = "11";
            november.MonthString = "November";
            monthList.Add(november);

            Month december = new Month();
            december.MonthValue = "12";
            december.MonthString = "December";
            monthList.Add(december);







            ViewBag.Months = monthList;
            ViewBag.Month = Month;
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
    }
}