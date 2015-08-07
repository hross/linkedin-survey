using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class Company
    {
        public int Id { get; set; }

        public string Industry { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public string Ticker { get; set; }

        public string Type { get; set; }
    }
}
