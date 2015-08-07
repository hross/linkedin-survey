using JobFinder.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobFinder.Web.Controllers
{
    public class AnswerController : ApiController
    {
        private AnswerService _service =
            new AnswerService(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);

        // GET: api/Answer
        public IEnumerable<Answer> Get(int? questionId)
        {
            return _service.Answers(questionId);
        }

        // GET: api/Answer/5
        public Answer Get(int id)
        {
            return _service.Answer(id);
        }

        // POST: api/Answer
        public void Post([FromBody]Answer value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Answer/5
        public void Put(int id, [FromBody]Answer value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Answer/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
