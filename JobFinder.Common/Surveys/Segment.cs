using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class Segment
    {
        public Segment()
        {
            this.Order = 0;
            this.ButtonText = "Click to get started!";
        }

        public long Id { get; set; }

        public long SurveyId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ButtonText { get; set; }
        
        [Ignore]
        public long NextId { get; set; }

        [Ignore]
        public long FirstQuestionId { get; set; }

        //public static int FirstQuestion(SurveyContext db, int id)
        //{
        //    var question = db.Questions.Where(q => q.SegmentId == id).OrderBy(q => q.Order).ThenBy(q => q.Id).FirstOrDefault();

        //    return question.Id;
        //}

        //public static Segment NextSegment(SurveyContext db, int segmentId = 0)
        //{
        //    return db.Segments.OrderBy(s => s.Order).ThenBy(s => s.Id).Where(s => s.Id > segmentId).Take(1).FirstOrDefault();
        //}
    }
}
