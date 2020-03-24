using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Event_Attendees_Tracker_DAL.Models;
using Event_Attendees_Tracker_DAL.util;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public static class ServiceQuery
    {
        private static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();

        /// <summary>
        /// Generating QR Code and sending mail
        /// </summary>
        public static void SendQREmail()
        {
            try
            {
                var today = DateTime.Now;

                var events = (from oneEvent in _eatDBContext.EventDetails
                              where EntityFunctions.TruncateTime(oneEvent.EventDate) == today.Date
                              select oneEvent).ToList();
                foreach ( var oneEvent in events )
                {
                    var activeEvents = (from activeEvent in _eatDBContext.EventAttendees
                                        where activeEvent.EventDetails.ID == oneEvent.ID && activeEvent.MailSent == false
                                        select activeEvent).ToList();
                    foreach ( var oneStudent in activeEvents )
                    {
                        var students = (from student in _eatDBContext.RegisteredStudents
                                        where student.ID == oneStudent.RegisteredStudents.ID
                                        select student.EmailID).ToList();
                        foreach ( var studentEmail in students )
                        {
                            Mailer.SendQRMail(studentEmail, oneEvent.EventName, oneEvent.ID, oneStudent.ID);

                            var student = _eatDBContext.EventAttendees.Where(val => val.ID == oneStudent.ID).Single<EventAttendees>();
                            student.MailSent = true;
                            _eatDBContext.SaveChanges();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
            }
        }
    }
}