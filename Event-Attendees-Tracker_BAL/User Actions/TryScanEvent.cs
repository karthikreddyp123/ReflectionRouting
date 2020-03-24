using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Event_Attendees_Tracker_BAL.User_Actions
{
    public class TryScanEvent : ITryScanEvent
    {
        public TryScanEvent()
        {
        }

        public string GetActiveEventDetails(int volunteerID)
        {
            try
            {
                //todo:inset where based in volunteer ID
                var eventData = JsonConvert.SerializeObject(Event_Attendees_Tracker_DAL.DBQueries.TryScanEvent.GetActiveEventDetails(volunteerID));
                return eventData;
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
            }
            return null;
        }
    }
}