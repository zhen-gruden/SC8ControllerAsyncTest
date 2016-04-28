using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sitecore.localhost;
using Sitecore.Web;

namespace ControllerTest.Controllers
{
    public class DummyController : SitecoreController
    {
        // GET: Dummy
        public async Task<ActionResult> Test()
        {
            WebClient webClient = new WebClient();

            var response =
                await
                    webClient.DownloadStringTaskAsync(
                        "http://maps.micello.com/webmapversion/scriptrequest?v=0&t=1461773950976&c=1#");

            ViewBag.html = response;

            return  View(); 

        }
    }
}