using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace UKADGroupTestTask.Sources
{
    public class PageParser : IReferencesProvider
    {
        private HtmlDocument _document;

        public PageParser()
        {
            _document = new HtmlDocument();
        }

        public IEnumerable<string> GetReferences(string domainUri, string html)
        {
            _document.LoadHtml(html);

            var references = _document.DocumentNode.SelectNodes("//a[@href]")
                .Select(node => node.Attributes["href"].Value)
                .Where(hrefvalue => hrefvalue.StartsWith(domainUri) || hrefvalue.StartsWith("/") && !hrefvalue.StartsWith("//"))

                .Select(value => value.Contains("#") ? value.Substring(0, (value.IndexOf("#"))) : value)
                .Select(value => value.TrimEnd('/'))
                .Where(value => value != "")
                .Select(value => value.StartsWith("/") ? value.Insert(0, domainUri) : value);

            return references;
        }
    }
}