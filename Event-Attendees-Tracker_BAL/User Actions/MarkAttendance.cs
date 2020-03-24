using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.User_Actions
{
    public class MarkAttendance : IMarkAttendance
    {
        public MarkAttendance()
        {
        }

        public string MarkStudentAttendance(String QRString)
        {
            try
            {
                return JsonConvert.SerializeObject(Event_Attendees_Tracker_DAL.DBQueries.MarkAttendance.MarkStudentAttendance(QRString));
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
                return null;
            }
        }
    }
}