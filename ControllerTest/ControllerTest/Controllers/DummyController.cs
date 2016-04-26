using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControllerTest.Controllers
{
    public class DummyController : SitecoreController
    {
        // GET: Dummy
        public async Task<ActionResult> Test()
        {
            // download a bunch of URLs in parallel with await
            var webClient = new WebClient();

            var urls = new[] {
                "https://google.com",
                "https://bing.com",
                "https://yahoo.com"
            }.Select(url => webClient.DownloadStringTaskAsync(url));

            var contents = await Task.WhenAll(urls);

            // or just await one task
            var google = await webClient.DownloadStringTaskAsync("https://google.com");

            // execution will pick up right here when all the awaited tasks are done - thawing the thread to finish execution

            return View(contents);
        }
    }
}