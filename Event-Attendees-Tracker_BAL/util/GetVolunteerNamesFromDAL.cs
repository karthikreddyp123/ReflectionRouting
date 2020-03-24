using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.DBQueries;
namespace Event_Attendees_Tracker_BAL.util
{
    public static class GetVolunteerNamesFromDAL
    {
        public static IQueryable GetVounteerList()
        {
            return VolunteerNames.GetVolunteerNames();
        }
    }
}
