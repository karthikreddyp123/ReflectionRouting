//System Namespace Imports
using System;
using System.Linq;
using System.Collections.Generic;
using  System.Diagnostics;

//Custom Namespace Imports
using Event_Attendees_Tracker_DAL.Database_Context;
using  Event_Attendees_Tracker_DAL.Models;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public class FetchActiveEvents:IFetchActiveEvents
    {
        static EAT_DBContext _eatDbContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();

        public List<EventDetails> GetActiveEvents(int userId)
        {
            try
            {
                DateTime today = DateTime.Now;
                   
                TimeSpan time = today.TimeOfDay;

                return _eatDbContext.EventDetails
                    .Where(events => events.CreatedBy == userId)
                .OrderBy(events => events.EventDate).ToList();

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                throw ex;
            }
        }
    }/* && events.EventDate >= today*/
   
}
