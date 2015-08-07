using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class Profile
    {
        public Profile()
        {
            this._id = 0;
            this.Positions = new PositionContainer();
        }

        private long _id;

        [PrimaryKey, AutoIncrement, JsonIgnore]
        public long Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;

                foreach (var val in this.Positions.Values)
                {
                    val.ProfileId = _id;
                }
            }
        }

        private long _userId;

        public long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;

                foreach (var val in this.Positions.Values)
                {
                    val.UserId = _userId;
                }
            }
        }

        //https://www.linkedin.com/profile/view?id=
        [JsonProperty(PropertyName="id")]
        public string LinkedInId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string Industry { get; set; }
        
        public string Summary { get; set; }

        [Ignore, JsonProperty(PropertyName="positions")]
        public PositionContainer Positions { get; set; }
    }
}
