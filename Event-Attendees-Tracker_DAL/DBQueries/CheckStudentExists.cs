using Event_Attendees_Tracker_DAL.Database_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public class CheckStudentExists
    {
        private static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = null;

        public CheckStudentExists(EAT_DBContext eatDbContext)
        {
            _eatDBContext = eatDbContext;

        }
        public bool UseInterface(ICheckStudentExists checkStudentExists)
        {
            return checkStudentExists.CheckStudent("Temp");
        }
        public bool CheckStudent(string EmailID)
        {
            var checkStudentQuery= _eatDBContext.RegisteredStudents.Where(m => m.EmailID.Equals(EmailID)).FirstOrDefault();
            if (checkStudentQuery == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckAttendee(string EmailID,int EventID)
        {
            var checkEmailquery = _eatDBContext.RegisteredStudents.Where(m => m.EmailID == EmailID).FirstOrDefault();
            var checkEventQuery = _eatDBContext.EventDetails.Where(m => m.ID == EventID).FirstOrDefault();
            var checkAttendee = _eatDBContext.EventAttendees.Where(m => (m.RegisteredStudents.ID == checkEmailquery.ID && m.EventDetails.ID == checkEventQuery.ID)).FirstOrDefault();
            if (checkAttendee == null)
            {
                return true;
            }
            return false;
        }
    }
}
