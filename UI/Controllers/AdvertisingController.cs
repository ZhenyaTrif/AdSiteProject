using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.ServiceReference1;

namespace UI.Controllers
{
    public class AdvertisingController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Qoute = GetQuote();
            return PartialView();
        }

        public string GetQuote()
        {
            QuoteServiceClient serviceObj = new QuoteServiceClient();

            var qoute = serviceObj.GetQuote();

            return qoute;
        }
    }
}