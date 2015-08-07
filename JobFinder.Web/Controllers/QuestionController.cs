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
    public class QuestionController : ApiController
    {
        //TODO: DI
        private QuestionService _service = 
            new QuestionService(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);

        // GET: api/Question
        public IEnumerable<Question> Get()
        {
            return _service.Questions();
        }

        // GET: api/Question/5
        public Question Get(long id)
        {
            return _service.Question(id);
        }

        // POST: api/Question
        public void Post([FromBody]Question value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Question/5
        public void Put(int id, [FromBody]Question value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Question/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
