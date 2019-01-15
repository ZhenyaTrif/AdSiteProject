using System;
using System.Collections.Generic;

namespace WcfServices
{
    public class Quotes
    {
        public List<string> quote;
        static Random rnd = new Random();

        public Quotes()
        {
            quote = new List<string>();

            quote.Add("Only I can change my life. No one can do it for me.");
            quote.Add("There is only one happiness in this life, to love and be loved.");
            quote.Add("We cannot put off living until we are ready.");
            quote.Add("Live life to the fullest, and focus on the positive.");
        }

        public string GetRandomQuote()
        {
            int place = rnd.Next(0, 3);
            return quote[place];
        }
    }
}
