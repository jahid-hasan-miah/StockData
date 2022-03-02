using HtmlAgilityPack;
using StockData.StockManaging.Entities;
using StockData.StockManaging.UnitOfWorks;
using System.Linq;

namespace StockData.StockManaging.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IStockManagingUnitOfWork _stockManagingUnitOfWork;
        public CompanyService(IStockManagingUnitOfWork stockManagingUnitOfWork)
        {
            _stockManagingUnitOfWork = stockManagingUnitOfWork;
        }
        public void FetchData()
        {
            var entity = _stockManagingUnitOfWork.Companies.GetById(1);
            if (entity == null)
            {
                var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";

                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                var resultNode = htmlDoc.DocumentNode.Descendants("table").FirstOrDefault(x => x.HasClass("table")).Descendants("tr");

                foreach (HtmlNode node in resultNode)
                {
                    if ((node.SelectSingleNode("td[1]") == null ? "" : node.SelectSingleNode("td[1]").InnerText) != "")
                    {
                        var stp = new Company
                        {
                            TradeCode = node.SelectSingleNode("td[2]") == null ? "" : node.SelectSingleNode("td[2]").InnerText
                        };
                        _stockManagingUnitOfWork.Companies.Add(stp);
                        _stockManagingUnitOfWork.Save();
                    }
                }
            }
        }
    }
}
