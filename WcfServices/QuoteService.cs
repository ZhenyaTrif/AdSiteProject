using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServices
{
    public class QuoteService : IQuoteService
    {
        static public Quotes quotes = new Quotes();
        public string GetQuote()
        {
            return quotes.GetRandomQuote();
        }
    }
}
