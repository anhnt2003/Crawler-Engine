namespace Crawler_Engine_v2.Dtos
{
    public class CrawlrInfo
    {
        public string LinkUrl { get; set; }

        public string PriceSelector { get; set; }

        public string OldPriceSelector { get; set; }

        public CrawlrInfo(string linkUrl, string priceSelector, string oldPriceSelector)
        {
            LinkUrl = linkUrl;
            PriceSelector = priceSelector;
            OldPriceSelector = oldPriceSelector;
        }

    }
}
