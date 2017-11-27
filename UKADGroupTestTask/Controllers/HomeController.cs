using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UKADGroupTestTask.DbModels;
using UKADGroupTestTask.Models;
using UKADGroupTestTask.Sources;

namespace UKADGroupTestTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProcessUri(string requestedUri)
        {
            var formattedUri = (new UriFormatter()).ToProperFormat(requestedUri);
            var requestedSite = GetRequestedSite(formattedUri);
            var resultModel = GetResultModel(requestedSite);

            ViewBag.ChartLabels = GetJSTypeArrayOf(resultModel.Pages.Select(page => page.Uri));
            ViewBag.ChartMaxValues = GetJSTypeArrayOf(resultModel.Pages.Select(page => page.RTMax));
            ViewBag.ChartMinValues = GetJSTypeArrayOf(resultModel.Pages.Select(page => page.RTMin));

            return PartialView(resultModel);
        }

        #region Additional Logic
        private Site GetRequestedSite(string uri)
        {
            Site resultSite;

            using (var siteDB = new SiteDBEntities())
            {
                siteDB.Configuration.ProxyCreationEnabled = false;
                if (siteDB.Sites.Find(uri) != null)
                    resultSite = siteDB.Sites.Include("Pages").Single(site => site.Uri == uri);

                else
                {
                    resultSite = siteDB.Sites.Create();
                    resultSite.Uri = uri;
                    resultSite.AddUniquePage(new Page() { Uri = uri });

                    var parser = new PageParser();
                    var refs = parser.GetReferences(uri, (new HtmlWebLoader()).GetHTML(uri));

                    resultSite.AddUniquePage(refs);
                    siteDB.Sites.Add(resultSite);
                }

                UpdateAverageResponceTime(resultSite);
                siteDB.SaveChanges();
            }

            return resultSite;
        }

        private void UpdateAverageResponceTime(Site site)
        {
            var requester = new Requester();
            foreach (var page in site.Pages)
            {
                page.UpdateResponceTime(requester.GetResponceTime(page.Uri));
                page.UpdateResponceTime(requester.GetResponceTime(page.Uri));
            }

            site.AverageResponseTime = site.Pages.Average(page => (page.ResponceTimeMin + page.ResponceTimeMax) / 2);
        }

        private ResultsModel GetResultModel(Site requestedSite)
        {
            int EvaluatedResultPosition = 0;

            using (var siteDB = new SiteDBEntities())
            {
                var count = siteDB.Sites.Count(site => site.AverageResponseTime > requestedSite.AverageResponseTime);

                var position = ((double)count / (double)siteDB.Sites.Count()) * 100;
                EvaluatedResultPosition = (int)position;
            }

            var resultModel = new ResultsModel
            {
                DomainUri = requestedSite.Uri,
                AverageResponceTime = requestedSite.AverageResponseTime,
                EvaluatedResult = EvaluatedResultPosition
            };
            resultModel.Pages = new HashSet<SimplePage>();

            foreach (var page in requestedSite.Pages.OrderByDescending(page => page.ResponceTimeMax))
                resultModel.Pages.Add(
                    new SimplePage
                    {
                        Uri = page.Uri,
                        RTMax = page.ResponceTimeMax,
                        RTMin = page.ResponceTimeMin
                    });

            return resultModel;
        }

        private string GetJSTypeArrayOf<T>(IEnumerable<T> collection)
        {
            var result = new StringBuilder(collection.Count() * 2 + 1);

            foreach (var item in collection)
                if (item is string)
                    result.Append("'").Append(item).Append("'").Append(',');
                else
                    result.Append(item).Append(',');

            return result.ToString();
        }
#endregion
    }
}