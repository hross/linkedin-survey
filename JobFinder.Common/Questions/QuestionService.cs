using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace JobFinder.Common
{
    public class QuestionService
    {
        private OrmLiteConnectionFactory _factory;

        public QuestionService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<Question>(overwrite: false);

                if (db.Count<Question>() == 0)
                {
                    db.Insert<Question>(new Question { Id = 1, Order = 1, SegmentId = 1, Text = "What is your favorite color?" });
                    db.Insert<Question>(new Question { Id = 2, Order = 2, SegmentId = 1, Text = "What is your quest?" });

                    db.Insert<Question>(new Question { Id = 3, Order = 1, SegmentId = 2, Text = "One more question?" });
                }
            });

            var svc = new AnswerService(connectionString);
        }
        
        public Question Question(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var question = db.Where<Question>(m => m.Id == id).FirstOrDefault();
                return PopulateDerived(question, db);
            }
        }

        public List<Question> Questions()
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return db.Select<Question>();
            }
        }

        public Question Add(Question Question)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<Question>(Question);

                Question.Id = db.GetLastInsertId();

                return Question;
            }
        }

        public Question Update(Question Question)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<Question>(Question);
                return Question;
            }
        }

        public bool Delete(Question Question)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Question>(Question);
                return true;
            }
        }

        public bool DeleteById(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Question>(s => s.Id == id);
                return true;
            }
        }

        private Question PopulateDerived(Question question, IDbConnection db)
        {
            question.Answers = Answers(question, db);
            question.NextId = NextQuestionId(question, db);
            return question;
        }
        
        private List<Answer> Answers(Question question, IDbConnection db)
        {
            if (null == question)
                return new List<Answer>();

            return db.Where<Answer>(a => a.QuestionId == question.Id).OrderBy(a => a.Order).ThenBy(a => a.Id).ToList();
        }

        public long NextQuestionId(Question question, IDbConnection db)
        {
            if (null == question)
                return 0;

            return db.QueryScalar<long>(@"SELECT TOP 1 Id FROM Question WHERE [Order] > @order AND SegmentId = @segmentId", new { order = question.Order, segmentId = question.SegmentId });
        }
    }
}