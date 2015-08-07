using JobFinder.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

namespace JobFinder.Web.Controllers
{
    public abstract class AuthenticatedApiController : ApiController
    {
        private JobUserStore _userStore;

        public AuthenticatedApiController()
        {
            _user = null;
            _userStore = new JobUserStore(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);
        }

        private JobUser _user;
        public JobUser CurrentUser
        {
            get
            {
                if (null == _user)
                {
                    var usr = this.User.Identity;
                    if (null == usr)
                        return null;

                    _user = _userStore.FindByNameAsync(usr.Name).Result;
                }

                return _user;
            }
        }
    }
}