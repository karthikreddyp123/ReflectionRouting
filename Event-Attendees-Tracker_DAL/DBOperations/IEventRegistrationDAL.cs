using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBOperations
{
    public interface IEventRegistrationDAL
    {
        Dictionary<string, string> InsertTblRegisteredStudents(DataTable studentsData);
        List<String> InsertTblEventAttendees(List<String> StudentList, int EventID);
    }
}
