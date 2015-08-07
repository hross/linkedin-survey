using JobFinder.Common;
using JobFinder.Common.LinkedIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=SurveyDB;User Id=surveyadmin;Password=Survey123;";
            //var service = new ExternalCredentialService(connectionString);

            //var credential = service.Credential(1);

            string authToken = "test";

            var profileService = new ProfileService(connectionString);
            profileService.CollectProfileData(authToken);
        }
    }
}
