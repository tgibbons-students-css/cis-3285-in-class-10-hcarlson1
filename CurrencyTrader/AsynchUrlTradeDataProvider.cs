using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTrader
{
    public class AsynchUrlTradeDataProvider : ITradeDataProvider
    {
        private readonly String url;
        UrlTradeDataProvider SynchTradeProvider;
        TradeDataUpdate tradeUpdater;
        public AsynchUrlTradeDataProvider(String url, TradeDataUpdate newTradeUpdater)
        {
            SynchTradeProvider = new UrlTradeDataProvider(url);
            this.url = url;
            tradeUpdater = newTradeUpdater;
        }
        public void GetTradeData()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(url);
            client.DownloadStringCompleted += DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(url));
            //Task.Run(() => SynchTradeProvider.GetTradeData());//this creates a blank named instance and now we just run the gettradedata once
            //return SynchTradeProvider.GetTradeData();
        }
        static void DownloadStringCompleted(object sender,
           DownloadStringCompletedEventArgs e)
        {

            string[] lines = e.Result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            tradeUpdater.UpdateTradeData(lines);
        }
    }
}
