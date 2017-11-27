using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKADGroupTestTask.DbModels
{
    public partial class Page
    {

        public void UpdateResponceTime(double newTime)
        {
            if (ResponceTimeMax == 0 && ResponceTimeMin == 0)
            {
                ResponceTimeMax = newTime;
                ResponceTimeMin = newTime;
            }

            if (ResponceTimeMin > newTime)
                ResponceTimeMin = newTime;

            if (ResponceTimeMax < newTime)
                ResponceTimeMax = newTime;
        }

        public override bool Equals(object obj)
        {
            return Uri.Equals((obj as Page).Uri);
        }
        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }
    } 
}