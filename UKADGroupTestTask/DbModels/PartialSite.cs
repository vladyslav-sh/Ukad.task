using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKADGroupTestTask.DbModels
{
    public partial class Site
    {

        public void AddUniquePage(Page page)
        {
            if (!Pages.Contains(page))
                Pages.Add(page);
        }

        public void AddUniquePage(IEnumerable<string> references)
        {
            foreach (var reference in references)
                AddUniquePage(new Page { Uri = reference });
        }

        public override bool Equals(object obj)
        {
            return Uri.Equals((obj as Site).Uri);
        }

        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }
    }
}