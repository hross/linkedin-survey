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
    public class QuestionAnswerController : AuthenticatedApiController
    {
        //TODO: DI
        private QuestionAnswerService _service =
            new QuestionAnswerService(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);

        // GET: api/Question
        public IEnumerable<QuestionAnswer> Get()
        {
            return _service.QuestionAnswers();
        }

        // GET: api/Question/5
        public QuestionAnswer Get(long id)
        {
            return _service.QuestionAnswer(id);
        }

        // POST: api/Question
        public void Post([FromBody]QuestionAnswer value)
        {
            value.UserId = this.CurrentUser.UserId;

            if (!_service.Contains(value.QuestionId))
                _service.Add(value);
        }

        // PUT: api/Question/5
        public void Put(int id, [FromBody]QuestionAnswer value)
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
