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
    public class SegmentController : ApiController
    {
        //TODO: DI
        private SegmentService _service = 
            new SegmentService(ConfigurationManager.ConnectionStrings["SurveyDataSource"].ConnectionString);

        // GET: api/Segment
        public IEnumerable<Segment> Get()
        {
            var sid = this.Request.RequestUri.ParseQueryString()["surveyId"];

            if (string.IsNullOrEmpty(sid)){
                return _service.Segments();
            } else {
                long surveyId = 0;
                long.TryParse(sid, out surveyId);
                return _service.Segments(surveyId);
            }
            
            
        }

        // GET: api/Segment/5
        public Segment Get(long id)
        {
            return _service.Segment(id);
        }

        // POST: api/Segment
        public void Post([FromBody]Segment value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Segment/5
        public void Put(int id, [FromBody]Segment value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Segment/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
