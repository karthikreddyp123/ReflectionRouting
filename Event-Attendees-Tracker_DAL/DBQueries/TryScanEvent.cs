using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Event_Attendees_Tracker_DAL.Models;
using Newtonsoft.Json;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public static class TryScanEvent
    {
        private static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();

        /// <summary>
        /// To get active event details to start to take attendance.
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns></returns>
        public static String GetActiveEventDetails(int volunteerID)
        {
            try
            {
                var today = DateTime.Now;

                //TODO: add volunteer id in query after updating model
                var activeEvent = (from oneEvent in _eatDBContext.EventDetails
                                   where EntityFunctions.TruncateTime(oneEvent.EventDate) == today.Date
                                   select oneEvent).ToList();
                return JsonConvert.SerializeObject(activeEvent);
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
                return null;
            }
        }
    }
}