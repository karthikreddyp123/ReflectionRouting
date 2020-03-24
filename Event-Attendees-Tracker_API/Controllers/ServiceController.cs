using Event_Attendees_Tracker_BAL.ServiceActions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Event_Attendees_Tracker_API.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly IMailer _iMailer;

        public ServiceController(IMailer iMailer)
        {
            this._iMailer = iMailer;
        }

        [Route("api/Service/SendQR/")]
        [HttpGet]
        public IHttpActionResult SendQR()
        {
            try
            {
                _iMailer.SendQREmail();

                return Content(HttpStatusCode.OK, new { });
            }
            catch ( Exception ex )
            {
                Debug.Print(ex.Message);
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}