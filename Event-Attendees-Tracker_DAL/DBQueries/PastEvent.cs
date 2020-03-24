using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.Models;
using Event_Attendees_Tracker_CustomResponseModel;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
   public class PastEvent
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();
        static DateTime today = DateTime.Now;
        static TimeSpan time = today.TimeOfDay;
        public static List<PastEventResponseModel> fetchPastEventAttendeesData(int userID)
        {
            try
            {
                var fetchPastEventDetails = _eatDBContext.EventDetails.Where(
                    events => events.CreatedBy == userID && (events.EventDate<today  || (events.EventDate==today && events.EndTime <= time)))
                .OrderByDescending(events => events.EventDate).ToList();
                List<PastEventResponseModel> pastEventResponseList = new List<PastEventResponseModel>();

                foreach (var eventData in fetchPastEventDetails)
                {
                    PastEventResponseModel pastEventResponseModel = new PastEventResponseModel();
                    pastEventResponseModel.EventID = eventData.ID;
                    pastEventResponseModel.eventName = eventData.EventName;
                    pastEventResponseModel.numberOfStudentsRegistered = _eatDBContext.EventAttendees.Where(eventAttendees => eventAttendees.EventDetails.ID == eventData.ID).Count();
                    pastEventResponseModel.numberOfStudentsPresent = _eatDBContext.EventAttendees.Where(eventAttendees => eventAttendees.EventDetails.ID == eventData.ID && eventAttendees.isPresent == true).Count();
                    pastEventResponseList.Add(pastEventResponseModel);
                }
                if (pastEventResponseList.Count == 0)
                {
                    return null;
                }

                return pastEventResponseList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        
    }
}
