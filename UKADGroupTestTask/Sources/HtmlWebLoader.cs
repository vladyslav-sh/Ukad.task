using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace UKADGroupTestTask.Sources
{
    public class HtmlWebLoader : IHtmlProvider
    {
        public string GetHTML(string adress)
        {
            using (var _webClient = new WebClient())
            {
                return _webClient.DownloadString(adress);
            }
        }
    }
}