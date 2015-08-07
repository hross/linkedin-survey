using Microsoft.AspNet.Identity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class JobUserStore : IUserStore<JobUser>, IUserLoginStore<JobUser>, IUserEmailStore<JobUser>
    {
        private OrmLiteConnectionFactory _factory;

        public JobUserStore(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<JobUser>(overwrite: false);
                db.CreateTable<ExternalCredential>(overwrite: false);
            });
        }

        #region IUserStore

        public Task CreateAsync(JobUser user)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<JobUser>(user);

                user.UserId = db.GetLastInsertId();

                return Task.FromResult(user);
            }
        }

        public Task DeleteAsync(JobUser user)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete(user);
                return Task.FromResult(1);
            }
        }

        public Task<JobUser> FindByIdAsync(string userId)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var user = db.Where<JobUser>(u => u.Email == userId).FirstOrDefault();
                return Task.FromResult(user);
            }
        }

        public Task<JobUser> FindByNameAsync(string userName)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var user = db.Where<JobUser>(u => u.Email == userName).FirstOrDefault();
                return Task.FromResult(user);
            }
        }

        public Task UpdateAsync(JobUser user)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<JobUser>(user);
                return Task.FromResult(user);
            }
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(JobUser user, UserLoginInfo login)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var cred = new ExternalCredential { 
                    JobUserId = user.UserId, 
                    LoginKey = login.ProviderKey, 
                    LoginProvider = login.LoginProvider 
                };

                db.Insert<ExternalCredential>(cred);

                cred.Id = db.GetLastInsertId();

                return Task.FromResult(cred);
            }
        }

        public Task<JobUser> FindAsync(UserLoginInfo login)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var cred = db.Where<ExternalCredential>(ec => ec.LoginProvider == login.LoginProvider && ec.LoginKey == login.ProviderKey).FirstOrDefault();

                if (null == cred)
                    return Task.FromResult<JobUser>(null);

                return Task.FromResult(db.Where<JobUser>(ju => ju.UserId == cred.JobUserId).FirstOrDefault());
            }
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(JobUser user)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return Task.FromResult((IList<UserLoginInfo>) db
                    .Where<ExternalCredential>(ec => ec.JobUserId == user.UserId)
                    .ConvertAll(ec => ec.ToLoginInfo()).ToList());
            }
        }

        public Task RemoveLoginAsync(JobUser user, UserLoginInfo login)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var count = db.Delete<ExternalCredential>(ec => 
                    ec.JobUserId == user.UserId &&
                    ec.LoginKey == login.ProviderKey &&
                    ec.LoginProvider == login.LoginProvider);

                return Task.FromResult(count);
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region IUserEmailStore

        public Task<JobUser> FindByEmailAsync(string email)
        {
            return this.FindByIdAsync(email);
        }

        public Task<string> GetEmailAsync(JobUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(JobUser user)
        {
            return Task.FromResult(true); // email is always confirmed
        }

        public Task SetEmailAsync(JobUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(JobUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
