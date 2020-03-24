using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public interface ICheckStudentExists
    {
        bool CheckStudent(string EmailID);
        bool CheckAttendee(string EmailID, int EventID);
    }
}
