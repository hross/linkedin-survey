using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class PositionContainer
    {
        public PositionContainer()
        {
            this.Values = new List<Position>();
        }

        [JsonProperty(PropertyName="_total")]
        int Total { get; set; }

        [JsonProperty(PropertyName = "values")]
        public List<Position> Values { get; set; }
    }
}
