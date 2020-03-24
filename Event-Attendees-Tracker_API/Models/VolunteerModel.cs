using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Attendees_Tracker_API.Models
{
    public class VolunteerModel
    {
        public int UserUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
    }
}