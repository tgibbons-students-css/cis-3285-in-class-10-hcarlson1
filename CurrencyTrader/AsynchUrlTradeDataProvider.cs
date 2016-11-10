using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTrader
{
    public class AsynchUrlTradeDataProvider : ITradeDataProvider
    {
        private readonly String url;
        UrlTradeDataProvider SynchTradeProvider;
        public AsynchUrlTradeDataProvider(String url)
        {
            SynchTradeProvider = new UrlTradeDataProvider(url);
            this.url = url;
        }
        public IEnumerable<string> GetTradeData()
        {
            Task.Run(() => SynchTradeProvider.GetTradeData());//this creates a blank named instance and now we just run the gettradedata once
            //return SynchTradeProvider.GetTradeData();
        }
    }
}
