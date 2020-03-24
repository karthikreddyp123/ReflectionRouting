using Event_Attendees_Tracker_API.Models;
using Event_Attendees_Tracker_BAL.User_Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Event_Attendees_Tracker_API.Controllers
{
    public class VolunteerController : ApiController
    {
        private readonly ITryScanEvent _event;
        private readonly IMarkAttendance _markAttendance;

        public VolunteerController(ITryScanEvent eventScan, IMarkAttendance markAttendance)
        {
            _event = eventScan;
            _markAttendance = markAttendance;
        }

        [Route("api/Volunteer/GetActiveEventDetails/{VolunteerID}")]
        [HttpGet]
        public IHttpActionResult GetActiveEventDetails(int VolunteerID)
        {
            try
            {
                var response = _event.GetActiveEventDetails(VolunteerID);
                if ( response == null )
                {
                    return Content(HttpStatusCode.BadRequest, new { Status = "NO_EVENTS", Message = "No Active Events Found for this Volunteer." });
                }
                else
                {
                    return Content(HttpStatusCode.OK, new { Status = "OK", Event = response });
                }
            }
            catch ( Exception ex )
            {
                Debug.Print(ex.Message);
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        [Route("api/Volunteer/MarkAttendance/{QRString}")]
        [HttpGet]
        public IHttpActionResult MarkAttendance(String QRString)
        {
            try
            {
                var response = _markAttendance.MarkStudentAttendance(QRString);
                if ( response == null )
                {
                    return Content(HttpStatusCode.BadRequest, "Failed.");
                }
                else
                {
                    return Content(HttpStatusCode.OK, new { Status = "OK" });
                }
            }
            catch ( Exception ex )
            {
                Debug.Print(ex.Message);
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}