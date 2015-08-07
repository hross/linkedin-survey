using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class LinkedInDate
    {
        public LinkedInDate()
        {
            Year = DateTime.UtcNow.Year;
            Month = 1;
            Day = 1;
        }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public DateTime Date()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }
    }
}
