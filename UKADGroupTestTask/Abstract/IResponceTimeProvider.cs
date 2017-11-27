using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKADGroupTestTask.Sources
{
    public interface IResponceTimeProvider
    {
        double GetResponceTime(string uri);
    }
}