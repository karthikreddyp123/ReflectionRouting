using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Attendees_Tracker.Modals.Response_Models
{
    public class EventAttendeesModelList
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailID { get; set; }
    }
}