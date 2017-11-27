using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

namespace UKADGroupTestTask.Sources
{
    public class Requester : IResponceTimeProvider
    {
        public double GetResponceTime(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 10000;
            Stopwatch timer = new Stopwatch();

            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
            
            timer.Stop();

            return (int)timer.Elapsed.TotalMilliseconds;
        }
    }
}