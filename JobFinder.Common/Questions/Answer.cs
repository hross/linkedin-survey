using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class Answer
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public int Order { get; set; }

        public long QuestionId { get; set; }
        
        public int Value { get; set; }

        public string Text { get; set; }
    }
}
