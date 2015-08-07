using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace JobFinder.Common
{
    public class SegmentService
    {
        private OrmLiteConnectionFactory _factory;

        public SegmentService(string connectionString)
        {
            _factory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            _factory.Run(db =>
            {
                db.CreateTable<Segment>(overwrite: false);

                if (db.Count<Segment>() == 0)
                {
                    db.Insert<Segment>(new Segment { Id = 1, SurveyId = 1, Order = 1, Name = "First Questions", Description = "Answer some simple questions.", ButtonText = "Start" });
                    db.Insert<Segment>(new Segment { Id = 2, SurveyId = 1, Order = 2, Name = "Second Questions", Description = "Answer some more complex questions.", ButtonText = "Start" });
                }
            });

            var svc = new QuestionService(connectionString);
        }

        public Segment Segment(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                var Segment = db.Where<Segment>(m => m.Id == id).FirstOrDefault();
                return PopulateDerived(Segment, db);
            }
        }

        public List<Segment> Segments(long surveyId = 0)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                if (surveyId < 0)
                    return db.Select<Segment>().ConvertAll(s => this.PopulateDerived(s, db));
                else
                    return db.Where<Segment>(s => s.SurveyId == surveyId).ToList().ConvertAll(s => this.PopulateDerived(s, db));
            }
        }

        public Segment Add(Segment segment)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Insert<Segment>(segment);

                segment.Id = db.GetLastInsertId();

                return segment;
            }
        }

        public Segment Update(Segment segment)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Update<Segment>(segment);
                return segment;
            }
        }

        public bool Delete(Segment segment)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Segment>(segment);
                return true;
            }
        }

        public bool DeleteById(long id)
        {
            using (IDbConnection db = _factory.OpenDbConnection())
            {
                db.Delete<Segment>(s => s.Id == id);
                return true;
            }
        }

        private Segment PopulateDerived(Segment segment, IDbConnection db)
        {
            segment.NextId = NextSegmentId(segment, db);
            segment.FirstQuestionId = FirstQuestionId(segment, db);
            return segment;
        }

        private long NextSegmentId(Segment segment, IDbConnection db)
        {
            if (null == segment)
                return 0;

            return db.QueryScalar<long>(@"SELECT TOP 1 Id FROM Segment WHERE [Order] > @order AND SurveyId = @surveyId", new { order = segment.Order, surveyId = segment.SurveyId });
        }

        private long FirstQuestionId(Segment segment, IDbConnection db)
        {
            return db.QueryScalar<long>(@"SELECT TOP 1 Id FROM Question WHERE SegmentId = @segmentId ORDER BY [Order],Id", new { segmentId = segment.Id });
        }
    }
}