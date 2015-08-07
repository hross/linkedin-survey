using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class Survey
    {
        public Survey()
        {
            this.ButtonText = "Click to get started!";
        }

        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }

        [Ignore]
        public long FirstSegmentId { get; set; }
       
    }
}
