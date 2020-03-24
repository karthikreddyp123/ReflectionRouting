using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.Models;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public static class VolunteerNames
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();
        public static IQueryable GetVolunteerNames()
        {

            List<string> query = new List<string>();
            var querytemp = (from roles in _eatDBContext.UserMappedRoles
                             join users in _eatDBContext.UserDetails on roles.UserDetails.ID equals users.ID
                             where roles.Master_DBRoles.ID == 3
                             select new { users.EmailID });
            return querytemp;


        }

    }
}
