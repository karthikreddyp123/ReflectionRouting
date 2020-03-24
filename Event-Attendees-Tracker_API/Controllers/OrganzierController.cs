using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Event_Attendees_Tracker_BAL.User_Actions;
using System.Web.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using Event_Attendees_Tracker_API.Models;
using Event_Attendees_Tracker_BAL.util;

namespace Event_Attendees_Tracker_API.Controllers
{
    public class OrganzierController : ApiController
    {
        public IQueryable GetDetailsOfStudents(int? EventId)
        {
            StudentListForTable studentList = new StudentListForTable();
            var responseData = studentList.fetchAttendeesData(Convert.ToInt32(EventId));
            return responseData;
        }
        //  api/Organizer/GetVolunteerMails
        public IQueryable GetVolunteerMails()
        {
            var volunteerList = GetVolunteerNamesFromDAL.GetVounteerList();
            var json = JsonConvert.SerializeObject(volunteerList);
            return volunteerList;
        }
        //api/Organizer/AddVolunteer
        public HttpResponseMessage AddVolunteer(VolunteerModel requestVolunteerData)
        {
            try
            {
                var response = AddingVolunteer.AddVolunteer(requestVolunteerData.FirstName, requestVolunteerData.LastName, requestVolunteerData.UserUID, requestVolunteerData.EmailID);
                if (!response) return Request.CreateResponse(HttpStatusCode.BadRequest, "Check the Data");
            }


            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Successfully Created!" });
        }
    
}
}
