using System;
using System.Diagnostics;

//Custom Namespace Imports
using Event_Attendees_Tracker_DAL.Models;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public class EventQuery:IEventQuery
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();
        public bool AddEvent(string eventName, string description, string venue, string posterImage, TimeSpan startTime, TimeSpan endTime, DateTime eventDate,int CreatedBy)
        {
            try
            {
                var responseData = _eatDBContext.EventDetails.Add(new EventDetails()
                {
                    EventName = eventName,
                    Description = description,
                    Venue = venue,
                    StartTime = startTime,
                    EndTime = endTime,
                    EventDate = eventDate,
                    PosterImage = posterImage,
                    CreatedAt = DateTime.Now  ,
                    CreatedBy=CreatedBy
                });
                _eatDBContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Debug.Print("Exception Occured");
                return false;
            }           
            
        }
    }
}
