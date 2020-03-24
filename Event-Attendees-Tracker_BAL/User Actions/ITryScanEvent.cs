using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.User_Actions
{
    public interface ITryScanEvent
    {
        string GetActiveEventDetails(int volunteerID);
    }
}