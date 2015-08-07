using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Common
{
    public class Question
    {
        public Question()
        {
            this.Order = 0;
            this.NextId = 0;
        }

        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public int Order { get; set; }
        
        public long SegmentId { get; set; }

        public string Text { get; set; }

        [Ignore]
        public virtual IList<Answer> Answers { get; set; }

        [Ignore]
        public long NextId { get; set; }

        [Ignore]
        public bool HasNextQuestion { get { return NextId > 0; } }
    }
}
