using JobFinder.Common.LinkedIn;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Contrib;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common.LinkedIn
{
    public class ProfileService
    {
        private OrmLiteConnectionFactory _factory;

        public ProfileService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<ProfileAuthData>(overwrite: false);
                db.CreateTable<Profile>(overwrite: false);
                db.CreateTable<Position>(overwrite: false);
                db.CreateTable<Company>(overwrite: false);
            });
        }


        private const string LinkedInBase = "https://www.linkedin.com/";
        private const string LinkedInRedirect = LinkedInBase + "uas/oauth2/authorization?response_type=code&client_id=YOUR_API_KEY&state=STATE&redirect_uri=YOUR_REDIRECT_URI";
        private const string LinkedInAuthRequest = "uas/oauth2/accessToken?grant_type=authorization_code&code=AUTHORIZATION_CODE&redirect_uri=YOUR_REDIRECT_URI&client_id=YOUR_API_KEY&client_secret=YOUR_SECRET_KEY";

        public void AssociateProfileAuthData(JobUser user, IEnumerable<Claim> claims)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                // get rid of previous auth
                db.Delete<ProfileAuthData>(auth => auth.UserId == user.UserId);

                // add this one
                db.Insert(new ProfileAuthData(user.UserId, claims));
            }
        }

        public void CollectProfileData(long userId)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var transaction = db.BeginTransaction();
                try
                {
                    var profileData = db.Where<ProfileAuthData>(auth => auth.UserId == userId).FirstOrDefault();

                    // no data to populate
                    if (null == profileData)
                        return;

                    var profile = this.CollectProfileData(profileData.AuthToken);
                    profile.UserId = userId;

                    // get rid of previous profile
                    db.Delete<Profile>(p => p.UserId == userId);
                    db.Delete<Position>(p => p.UserId == userId);

                    // add this one
                    db.Insert(profile);

                    profile.Id = db.GetLastInsertId();

                    foreach (var position in profile.Positions.Values)
                    {
                        // add each position
                        db.Insert(position);

                        db.Delete<Company>(c => c.Id == position.Company.Id);
                        db.Insert(position.Company);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// TODO: return a represntative object and test this.
        /// </summary>
        /// <param name="accessToken"></param>
        public Profile CollectProfileData(string accessToken)
        {
            // linkedin api request for a bunch of profile data related to this person
            var client = new RestClient("https://api.linkedin.com/v1/");
            var request = new RestRequest("people/~:(id,first-name,last-name,industry,location,headline,summary,specialties,main-address,positions,email-address,interests,skills,phone-numbers)", Method.GET);
            request.AddParameter("oauth2_access_token", accessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-li-format", "json");

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<Profile>(response.Content);
        }
    }
}
