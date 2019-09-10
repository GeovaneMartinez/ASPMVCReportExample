using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class UpdateActualController : Controller
    {
        AlpacaEntities dbcontext = new AlpacaEntities();

        public ActionResult InsertNewTrades()
        {
            List<ActualTransactions> transactionsDetails = new List<ActualTransactions>();

            var transactions = dbcontext.GetTrades();

            foreach (var item in transactions)
            {
                ActualTransactions details = new ActualTransactions();
                details.ID = item.ID;
                details.Date = item.Date;
                details.StartingLABUPrice = item.StartingLABUPrice;
                details.StartingLABDPrice = item.StartingLABDPrice;
                details.Strategy = item.Strategy;
                details.StartingType = item.StartingType;
                details.Shares = item.Shares;
                details.EndingLABUPrice = item.EndingLABUPrice;
                details.EndingLABDPrice = item.EndingLABDPrice;
                details.Type = item.Type;
                details.Reason = item.Reason;
                details.ActualLABUStartingPrice = "";
                details.ActualLABDStartingPrice = "";
                details.ActualLABUEndingPrice = "";
                details.ActualLABDEndingPrice = "";
                details.EndDate = item.EndDate;

                transactionsDetails.Add(details);

            }

            transactionsDetails.OrderBy(t => t.ID);
            ActualTransactions lastRecord = dbcontext.ActualTransactions.OrderByDescending(x => x.ID).FirstOrDefault();
            int lastRecordID = lastRecord.ID;
            string lastRecordTimestamp = lastRecord.Date;

            bool DateMatch = false;
            int counter = 0;
            int lastIDinActualTransactionTable = 0;
            int numbOfNewTrades = 0;

            foreach (var item in transactionsDetails)
            {
                //if(item.ID > lastRecordID)
                //{
                //    dbcontext.ActualTransactions.Add(item);
                //    dbcontext.SaveChanges();
                //}

                if(item.Date.Equals(lastRecordTimestamp))
                {
                    DateMatch = true;
                    lastIDinActualTransactionTable = lastRecordID;
                }

                if(DateMatch)
                {
                    counter++;
                }

                if(counter > 1)
                {
                    lastIDinActualTransactionTable++;
                    item.ID = lastIDinActualTransactionTable;
                    dbcontext.ActualTransactions.Add(item);
                    dbcontext.SaveChanges();
                    numbOfNewTrades++;

                }
            }

            string jsonMessage = "";
            if(numbOfNewTrades == 1)
            {
                jsonMessage = "Inserted " + numbOfNewTrades + " new trade. ";
            }
            else
            {
                jsonMessage = "Inserted " + numbOfNewTrades + " new trades. ";
            }

            return Json(new { success = jsonMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}