using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace JobFinder.Common
{
    public class AnswerService
    {
        private OrmLiteConnectionFactory _factory;

        public AnswerService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<Answer>(overwrite: false);

                if (db.Count<Answer>() == 0)
                {
                    db.Insert<Answer>(new Answer { Id = 1, QuestionId = 1, Text = "Green" });
                    db.Insert<Answer>(new Answer { Id = 2, QuestionId = 1, Text = "Yellow" });
                    db.Insert<Answer>(new Answer { Id = 3, QuestionId = 2, Text = "I seek the grail." });
                    db.Insert<Answer>(new Answer { Id = 4, QuestionId = 2, Text = "I don't know." });
                    db.Insert<Answer>(new Answer { Id = 5, QuestionId = 3, Text = "Yes" });
                    db.Insert<Answer>(new Answer { Id = 6, QuestionId = 3, Text = "No" });
                }
            });
        }

        public Answer Answer(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return db.Where<Answer>(m => m.Id == id).FirstOrDefault();
            }
        }

        public List<Answer> Answers(int? questionId)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                if (questionId.HasValue)
                    return db.Where<Answer>(a => a.QuestionId == questionId);
                else
                    return db.Select<Answer>();
            }
        }

        public Answer Add(Answer Answer)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<Answer>(Answer);

                Answer.Id = db.GetLastInsertId();

                return Answer;
            }
        }

        public Answer Update(Answer Answer)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<Answer>(Answer);
                return Answer;
            }
        }

        public bool Delete(Answer Answer)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Answer>(Answer);
                return true;
            }
        }

        public bool DeleteById(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Answer>(s => s.Id == id);
                return true;
            }
        }
    }
}