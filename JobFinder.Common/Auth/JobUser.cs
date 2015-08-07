using Microsoft.AspNet.Identity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class JobUser : IUser
    {
        /// <summary>
        /// Tell the ASP.NET identity stuff how to generate an identity cookie from this user.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<JobUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [PrimaryKey, AutoIncrement]
        public long UserId { get; set; }

        [Ignore]
        public string Id { get { return this.Email; } } // user email as primary ID for lookups and uniqueness

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
