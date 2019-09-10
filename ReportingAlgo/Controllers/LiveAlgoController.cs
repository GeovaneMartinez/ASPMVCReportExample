using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingAlgo.Controllers
{
    public class LiveAlgoController : Controller
    {
        AlpacaEntities dbcontext = new AlpacaEntities();

        public ActionResult GetNewStartTrades()
        {
           List<StartingPosition> startPositions =  dbcontext.StartingPosition.OrderByDescending(t => t.ID).Take(15).ToList();
           List<StartingPosition> startPositionsAsc =  startPositions.OrderBy(t => t.ID).ToList();

            return Json(startPositionsAsc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewCloseTrades()
        {
            List<ClosingPosition> closingPositions = dbcontext.ClosingPosition.OrderByDescending(t => t.ID).Take(15).ToList();
            List<ClosingPosition> closingPositionsAsc = closingPositions.OrderBy(t => t.ID).ToList();

            return Json(closingPositionsAsc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewActualTrades()
        {
            List<ActualTransactions> actualTransactions = dbcontext.ActualTransactions.OrderByDescending(t => t.ID).Take(15).ToList();
            List<ActualTransactions> actualTransactionsAsc = actualTransactions.OrderBy(t => t.ID).ToList();

            return Json(actualTransactionsAsc, JsonRequestBehavior.AllowGet);
        }


    }
}