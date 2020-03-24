using shortid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.DBQueries;

namespace Event_Attendees_Tracker_BAL.User_Actions
{
    public class AddingVolunteer
    {
        public static bool AddVolunteer(string FirstName, string LastName, int UserUID, string MailID)
        {
            try
            {
                string password = ShortId.Generate(true, true, 11).ToString();
                var responseAddEventData = AddingVoluneerQuery.AddVolunteer(FirstName, LastName, UserUID, MailID, password);
                return responseAddEventData;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }
    }
}
