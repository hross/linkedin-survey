using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace JobFinder.Common
{
    public class QuestionAnswerService
    {
        private OrmLiteConnectionFactory _factory;

        public QuestionAnswerService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<QuestionAnswer>(overwrite: false);
            });

            var svc = new AnswerService(connectionString);
        }
        
        public QuestionAnswer QuestionAnswer(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var qa = db.Where<QuestionAnswer>(m => m.Id == id).FirstOrDefault();
                return qa;
            }
        }

        public List<QuestionAnswer> QuestionAnswers()
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return db.Select<QuestionAnswer>();
            }
        }

        public QuestionAnswer Add(QuestionAnswer qa)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<QuestionAnswer>(qa);

                qa.Id = db.GetLastInsertId();

                return qa;
            }
        }

        public bool Contains(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return db.Count<QuestionAnswer>(qa => qa.QuestionId == id) > 0;
            }
        }

        public QuestionAnswer Update(QuestionAnswer qa)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<QuestionAnswer>(qa);
                return qa;
            }
        }

        public bool Delete(QuestionAnswer qa)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<QuestionAnswer>(qa);
                return true;
            }
        }

        public bool DeleteById(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<QuestionAnswer>(s => s.Id == id);
                return true;
            }
        }
    }
}