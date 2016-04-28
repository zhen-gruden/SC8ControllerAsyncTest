using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ControllerTest.Controllers
{
    public static class WebAccess
    {
        static WebClient _instance;
        public static WebClient GetWebClient()
        {
            if (_instance == null)
                _instance = new WebClient();

            return _instance;
        }
    }
}