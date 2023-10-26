namespace Crawler_Engine_v2.Dtos
{
    public class CrawlResult
    {
        public string LinkUrl { get; set; }

        public double Price { get; set; }

        public double OldPrice { get; set; }

        public double TimeCrawl { get; set; }
    }

    public class CrawlResultPage<T>
    {
        public double TotalTimeCrawler { get; set; }

        public string Error { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
