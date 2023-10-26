using Crawler_Engine_v2.Dtos;
using System.Security.Cryptography;

namespace Crawler_Engine_v2.Common
{
    public class CrawlerListProvider
    {
        public string Provider { get; set; }
        public CrawlerListProvider(string provider)
        {
            Provider = provider;
        }

        public static List<CrawlrInfo> ListProvider() => new List<CrawlrInfo>()
        {
            new CrawlrInfo(
                "laptop88", 
                "body > main > div.product-detail > div > div.main-product-detail.d-flex > div.main-product-mid > div.price.js-price-config.js-price-buildpc",
                "body > main > div.product-detail > div > div.main-product-detail.d-flex > div.main-product-mid > div.main-price.d-flex.align-items > del"),
            new CrawlrInfo(
                "fptshop",
                "#root > main > div > div.l-pd-header > div:nth-child(2) > div.l-pd-row.clearfix > div.l-pd-right > div.st-price > div > div.st-price-main",
                "#root > main > div > div.l-pd-header > div:nth-child(2) > div.l-pd-row.clearfix > div.l-pd-right > div.st-price > div > div.st-price-sub > strike"),
             new CrawlrInfo(
                "thegioididong",
                "body > section.detail > div.box_main > div.box_right > div.box04.box_normal > div.price-one > div > p",
                ">"),
             new CrawlrInfo(
                "phongvu",
                "#__next > div > div > div > div > div:nth-child(4) > div.css-rf24tk > div > div:nth-child(2) > div.css-1hwtax5 > div > div > div.css-6b3ezu > div.css-2zf5gn > div.att-product-detail-latest-price.css-roachw",
                "#__next > div > div > div > div > div:nth-child(4) > div.css-rf24tk > div > div:nth-child(2) > div.css-1hwtax5 > div > div > div.css-6b3ezu > div.css-2zf5gn > div.css-3mjppt > div.att-product-detail-retail-price.css-18z00w6")
        };
    }
}
