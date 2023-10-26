using Crawler_Engine_v2.Dtos;
using Crawler_Engine_v2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler_Engine_v2.Controllers
{
    public class CrawlerController : ControllerBase
    {
        private readonly ICrawlerService _crawlerService;
        public CrawlerController(ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }

        [HttpPost("crawler")]
        public async Task<IActionResult> CrawlerEngine([FromBody]string url)
        {
            try
            {
                var result = await _crawlerService.Crawler(url);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("crawler-multi")]
        public IActionResult CrawlerEngineMultiLink([FromBody] string urls)
        {
            try
            {
                var result = _crawlerService.CrawlMultiLink(urls);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
