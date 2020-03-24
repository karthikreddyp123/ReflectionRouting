using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public interface IEventQuery
    {
        bool AddEvent(string eventName, string description, string venue, string posterImage, TimeSpan startTime,
            TimeSpan endTime, DateTime eventDate, int CreatedBy);
    }
}
