using Event_Attendees_Tracker_DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public static class AddingVoluneerQuery
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();
        public static bool AddVolunteer(string firstName, string lastName, int userUID, string mailID, string password)
        {
            try
            {
                var responseData = _eatDBContext.UserDetails.Add(new UserDetails()
                {
                    UserUID = userUID,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailID = mailID,
                    Password = password,
                    CreatedDate = DateTime.Now.Date,
                    UpdatedDate = DateTime.Now.Date
                });
                _eatDBContext.SaveChanges();
                var userID = _eatDBContext.UserDetails.Where(m => m.EmailID.Equals(mailID)).FirstOrDefault();
                var role = _eatDBContext.Master_DBRoles.Where(m => m.RoleName.Equals("Volunteer")).FirstOrDefault();
                var responseData1 = _eatDBContext.UserMappedRoles.Add(new UserMappedRoles()
                {
                    UserDetails = userID,
                    Master_DBRoles = role


                });
                _eatDBContext.SaveChanges();

                return true;
            }

            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }
    }
}
