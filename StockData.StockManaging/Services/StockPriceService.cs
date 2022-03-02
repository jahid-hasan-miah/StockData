using HtmlAgilityPack;
using StockData.StockManaging.Entities;
using StockData.StockManaging.UnitOfWorks;
using System.Linq;

namespace StockData.StockManaging.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly IStockManagingUnitOfWork _stockManagingUnitOfWork;
        public StockPriceService(IStockManagingUnitOfWork stockManagingUnitOfWork)
        {
            _stockManagingUnitOfWork = stockManagingUnitOfWork;
        }
        public void FetchData()
        {
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);
            var statusNode = htmlDoc.DocumentNode.SelectNodes("//div[@class='HeaderTop']").ToArray();
            var Status= statusNode.Last().SelectSingleNode("//span/span").InnerText;
            if(Status != "Closed")
            {
                var ResultStockPrice = htmlDoc.DocumentNode.Descendants("table").FirstOrDefault(x => x.HasClass("table")).Descendants("tr");

                foreach (HtmlNode node in ResultStockPrice)
                {
                    if ((node.SelectSingleNode("td[1]") == null ? "" : node.SelectSingleNode("td[1]").InnerText) != "")
                    {
                        var stp = new StockPrice
                        {
                            CompanyId = node.SelectSingleNode("td[1]") == null ? "" : node.SelectSingleNode("td[1]").InnerText,
                            LastTradingPrice = node.SelectSingleNode("td[3]") == null ? "" : node.SelectSingleNode("td[3]").InnerText,
                            High = node.SelectSingleNode("td[4]") == null ? "" : node.SelectSingleNode("td[4]").InnerText,
                            Low = node.SelectSingleNode("td[5]") == null ? "" : node.SelectSingleNode("td[5]").InnerText,
                            ClosePrice = node.SelectSingleNode("td[6]") == null ? "" : node.SelectSingleNode("td[6]").InnerText,
                            YesterdayClosePrice = node.SelectSingleNode("td[7]") == null ? "" : node.SelectSingleNode("td[7]").InnerText,
                            Change = node.SelectSingleNode("td[8]") == null ? "" : node.SelectSingleNode("td[8]").InnerText,
                            Trade = node.SelectSingleNode("td[9]") == null ? "" : node.SelectSingleNode("td[9]").InnerText,
                            Value = node.SelectSingleNode("td[10]") == null ? "" : node.SelectSingleNode("td[10]").InnerText,
                            Volume = node.SelectSingleNode("td[11]") == null ? "" : node.SelectSingleNode("td[11]").InnerText
                        };
                        _stockManagingUnitOfWork.StockPrices.Add(stp);
                        _stockManagingUnitOfWork.Save();
                    }
                }
            }  
        }
    }
}
