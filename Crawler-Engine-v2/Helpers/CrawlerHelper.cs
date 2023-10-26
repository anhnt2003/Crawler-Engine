using System.Text.RegularExpressions;

namespace Crawler_Engine_v2.Helpers
{
    public static class CrawlerHelper
    {
        public static double NormalizeResult(string str)
        {
            return double.Parse(Regex.Replace(str, @"[^0-9\.]+", string.Empty).Replace(".", string.Empty));
        }
    }
}
