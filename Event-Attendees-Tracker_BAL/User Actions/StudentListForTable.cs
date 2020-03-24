using Event_Attendees_Tracker_DAL.DBQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.User_Actions
{
    public class StudentListForTable
    {
        public IQueryable fetchAttendeesData(int EventId)
        {
            StudentDetails studentDetails = new StudentDetails();
            return studentDetails.GetEventAttendeesList(EventId);
        }
    }
}
