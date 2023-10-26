using Crawler_Engine_v2.Common;
using Crawler_Engine_v2.Dtos;
using Crawler_Engine_v2.Helpers;
using PuppeteerSharp;
using System.Diagnostics;

namespace Crawler_Engine_v2.Services
{
    public class CrawlerService : ICrawlerService
    {
        private IBrowser browser;
        public CrawlerService()
        {
            InitializeBrowser();
        }
        public async Task<CrawlResult> Crawler(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Tham số không hợp lệ");
            }

            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var param = CrawlerListProvider
                    .ListProvider()
                    .Select(s => new { s.PriceSelector, s.OldPriceSelector, s.LinkUrl })
                    .Where(u => url.Contains(u.LinkUrl))
                    .FirstOrDefault();
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync(url, new NavigationOptions
                    {
                        WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded }
                    });
                    var price = await page.QuerySelectorAsync(param.PriceSelector).EvaluateFunctionAsync<string>("node => node.innerText"); ;
                    var oldPrice = await page.QuerySelectorAsync(param.OldPriceSelector).EvaluateFunctionAsync<string>("node => node.innerText"); ;
                    await page.CloseAsync();
                    watch.Stop();
                    return new CrawlResult
                    {
                        LinkUrl = url,
                        Price = CrawlerHelper.NormalizeResult(price),
                        OldPrice = CrawlerHelper.NormalizeResult(oldPrice),
                        TimeCrawl = watch.Elapsed.TotalSeconds
                    };
                }
            } 
            catch(Exception ex)
            {
                throw new Exception(url + ex);
            }
        }

        public CrawlResultPage<CrawlResult> CrawlMultiLink(string urls)
        {
            var watch = new Stopwatch();
            watch.Start();
            string[] urlsInfo = urls.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<CrawlResult> results = new List<CrawlResult>();
            var error = string.Empty;

            Parallel.ForEach(urlsInfo, url =>
            {
                var crawlResult = Task.Run(async () =>
                {
                    try
                    {
                        return await Crawler(url);
                    }
                    catch (Exception ex)
                    {
                        error += ex.Message;
                        return null;
                    }
                }).Result;

                if (crawlResult != null)
                {
                    results.Add(crawlResult);
                }
            });

            watch.Stop();

            return new CrawlResultPage<CrawlResult>
            {
                TotalTimeCrawler = watch.Elapsed.TotalSeconds,
                Data = results,
                Error = error
            };
        }
        private async void InitializeBrowser()
        {
            var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadAsync().GetAwaiter().GetResult();

            var launchOptions = new LaunchOptions
            {
                Headless = true,
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-infobars",
                    "--disable-setuid-sandbox",
                    "--ignore-certificate-errors",
                }
            };
            browser = Puppeteer.LaunchAsync(launchOptions).GetAwaiter().GetResult();
        }
    }
}
