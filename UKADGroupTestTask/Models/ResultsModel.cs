using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using UKADGroupTestTask.DbModels;

namespace UKADGroupTestTask.Models
{
    public class ResultsModel
    {
        public string DomainUri { get; set; }
        public double AverageResponceTime { get; set; }
        public int EvaluatedResult { get; set; }

        public HashSet<SimplePage> Pages { get; set; }

    }

    public class SimplePage
    {
        public string Uri { get; set; }
        public double RTMin { get; set; }
        public double RTMax { get; set; }

    }

}