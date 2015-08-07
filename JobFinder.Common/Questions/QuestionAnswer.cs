using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobFinder.Common {

    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
        }

        public QuestionAnswer(Question question)
        {
            this.QuestionId = question.Id;
        }

        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public long QuestionId { get; set; }

        public long AnswerId { get; set; }

        public long UserId { get; set; }
    }
}
