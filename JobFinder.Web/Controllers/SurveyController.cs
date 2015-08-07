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
    public class SurveyController : ApiController
    {
        //TODO: DI
        private SurveyService _service =
            new SurveyService(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);

        // GET: api/Survey
        public IEnumerable<Survey> Get()
        {
            return _service.Surveys();
        }

        // GET: api/Survey/5
        public Survey Get(long id)
        {
            return _service.Survey(id);
        }

        // POST: api/Survey
        public void Post([FromBody]Survey value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Survey/5
        public void Put(int id, [FromBody]Survey value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Survey/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
