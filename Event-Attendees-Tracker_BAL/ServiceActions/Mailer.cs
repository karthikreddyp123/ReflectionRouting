using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.ServiceActions
{
    public class Mailer : IMailer
    {
        /// <summary>
        /// To Send QR Email
        /// </summary>
        public void SendQREmail()
        {
            try
            {
                Event_Attendees_Tracker_DAL.DBQueries.ServiceQuery.SendQREmail();
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
            }
        }
    }
}