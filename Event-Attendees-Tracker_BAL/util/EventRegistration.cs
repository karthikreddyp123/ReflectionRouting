﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.DBOperations;
using System.Diagnostics;

namespace Event_Attendees_Tracker_BAL.util
{
    public class EventRegistration : IEventRegistration
    {
        private readonly IMailSend _mailSend;
        private readonly IEventRegistrationDAL _eventRegistrationDal;
        public EventRegistration(IMailSend mailSend,IEventRegistrationDAL eventRegistrationDal)
        {
            _mailSend = mailSend;
            _eventRegistrationDal = eventRegistrationDal;
        }

        public Dictionary<string, string> InsertTblRegisteredStudents(DataTable StudentRegistrationData, int EventID, string EventName)
        {
            // EventRegistrationDAL eventRegistration = new EventRegistrationDAL();
            Dictionary<String, String> returnData = new Dictionary<String, String>();
            List<String> EmailList = new List<String>();
            Dictionary<String, String> studentsInsertReturnValue = _eventRegistrationDal.InsertTblRegisteredStudents(StudentRegistrationData);
            foreach (var item in studentsInsertReturnValue.Keys)
            {
                if (studentsInsertReturnValue[item].Equals("Invalid Data"))
                {
                    returnData.Add(item, studentsInsertReturnValue[item]);
                }
                else
                {
                    EmailList.Add(item);
                }
            }
            if (EmailList.Count > 0)
            {
                var AttenddesInsertList = _eventRegistrationDal.InsertTblEventAttendees(EmailList, EventID);
                if (AttenddesInsertList.Count > 0)
                {
                    _mailSend.SendRegistrationMail(AttenddesInsertList, EventID);
                    Debug.Print("Succesfully added data into Attendees Table");
                }
                else
                {
                    Debug.Print("Could not insert data into Attendees Table");
                }
            }
            else
            {
                Debug.Print("Could not insert data into Registration Table");
            }
            return returnData;
        }

    }
}
