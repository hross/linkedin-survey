using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class ProfileAuthData
    {
        public ProfileAuthData()
        {
            this.UserId = 0;
            this.LastRefreshUTC = DateTime.UtcNow;
        }

        public ProfileAuthData(long userId, IEnumerable<Claim> claims)
        {
            this.UserId = userId;

            foreach (var claim in claims)
            {
                if (claim.Type == "urn:linkedin:accesstoken")
                {
                    this.AuthToken = claim.Value;
                }
            }

            this.LastRefreshUTC = DateTime.UtcNow;
        }

        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public long UserId { get; set; }

        public string AuthToken { get; set; }

        public DateTime LastRefreshUTC { get; set; }
    }
}
