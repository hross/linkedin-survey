using Microsoft.AspNet.Identity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class ExternalCredential
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public long JobUserId { get; set; }

        public string LoginKey { get; set; }

        public string LoginProvider { get; set; }

        public UserLoginInfo ToLoginInfo()
        {
            return new UserLoginInfo(this.LoginProvider, this.LoginKey);
        }
    }
}
