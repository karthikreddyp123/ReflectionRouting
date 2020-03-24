using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Event_Attendees_Tracker_BAL.User_Actions;
using Event_Attendees_Tracker_CustomResponseModel;
using Newtonsoft.Json;

namespace Event_Attendees_Tracker_API.Controllers
{
    public class EventController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage PastEventAttendees (int userId)
        {
            try
            {
                var pastEventAttendeesData = FetchPastEvents.fetchPastEventAttendeesData(userId);
                if (pastEventAttendeesData != null) 
                {
                    return Request.CreateResponse(HttpStatusCode.OK, pastEventAttendeesData);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new { Status = false});
                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
        }

    }
}
