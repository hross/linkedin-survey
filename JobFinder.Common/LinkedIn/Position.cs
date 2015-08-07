using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class Position
    {
        public Position()
        {
            this.StartDate = new LinkedInDate();
        }

        [JsonIgnore]
        public long ProfileId { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        public string Id { get; set; }

        [Ignore]
        public LinkedInDate StartDate { get; set; }

        [JsonIgnore]
        public int StartYear { get { return this.StartDate.Year; } set { this.StartDate.Year = value; } }

        [JsonIgnore]
        public int StartMonth { get { return this.StartDate.Month; } set { this.StartDate.Month = value; } }

        [JsonIgnore]
        public int StartDay { get { return this.StartDate.Day; } set { this.StartDate.Day = value; } }

        public bool IsCurrent { get; set; }

        public string Summary { get; set; }

        public string Title { get; set; }

        private int _companyId;

        [JsonIgnore]
        public int CompanyId
        {
            get
            {
                return (this.Company == null) ? _companyId : this.Company.Id;
            }
            set
            {
                this._companyId = value;
            }
        }

        [Ignore]
        public Company Company { get; set; }
    }
}
