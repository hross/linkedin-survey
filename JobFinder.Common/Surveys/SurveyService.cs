using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace JobFinder.Common
{
    public class SurveyService
    {
        private OrmLiteConnectionFactory _factory;

        public SurveyService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<Survey>(overwrite: false);

                if (db.Count<Survey>() == 0)
                {
                    db.Insert<Survey>(new Survey { Id = 1, Name = "My First Survey", Description = "This is the first survey.", ButtonText = "Let's Get Started!"});
                }
            });

            var svc = new SegmentService(connectionString);
        }

        public Survey Survey(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var Survey = db.Where<Survey>(m => m.Id == id).FirstOrDefault();
                return PopulateDerived(Survey, db);
            }
        }

        public List<Survey> Surveys()
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                return db.Select<Survey>();
            }
        }

        public Survey Add(Survey survey)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<Survey>(survey);

                survey.Id = db.GetLastInsertId();

                return survey;
            }
        }

        public Survey Update(Survey survey)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<Survey>(survey);
                return survey;
            }
        }

        public bool Delete(Survey survey)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Survey>(survey);
                return true;
            }
        }

        public bool DeleteById(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Survey>(s => s.Id == id);
                return true;
            }
        }

        private Survey PopulateDerived(Survey survey, IDbConnection db)
        {
            survey.FirstSegmentId = FirstSegmentId(survey, db);
            return survey;
        }

        private long FirstSegmentId(Survey survey, IDbConnection db)
        {
            return db.QueryScalar<long>(@"SELECT TOP 1 Id FROM Segment WHERE SurveyId = @surveyId ORDER BY [Order],Id", new { surveyId = survey.Id });
        }
    }
}