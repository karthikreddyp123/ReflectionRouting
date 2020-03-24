using Event_Attendees_Tracker_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL
{
    public interface IFetchActiveEvents
    {
        List<EventDetails> GetActiveEvents(int userId);
    }
}
