using Crawler_Engine_v2.Dtos;

namespace Crawler_Engine_v2.Services
{
    public interface ICrawlerService
    {
        Task<CrawlResult> Crawler(string url);

        CrawlResultPage<CrawlResult> CrawlMultiLink(string urls);
    }
}
