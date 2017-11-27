using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKADGroupTestTask.Sources
{
    interface IReferencesProvider
    {
        IEnumerable<string> GetReferences(string uri, string html);
    }
}
