using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public class MarkAttendance

    {
        private static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = Event_Attendees_Tracker_DAL.Instances.DBInstance.getDBInstance();

        /// <summary>
        /// To mark student attendance when the QR is scanned at event.
        /// </summary>
        /// <param name="QRString"></param>
        /// <returns></returns>
        public static bool MarkStudentAttendance(String QRString)
        {
            try
            {
                var element = UnpackQRString(Decrypt(QRString));
                var eventID = Int32.Parse(element[0]);
                var studentID = Int32.Parse(element[1]);
                var eventData = _eatDBContext.EventAttendees.Where(m => m.EventDetails.ID == eventID && m.RegisteredStudents.ID == studentID)
                    .FirstOrDefault();
                eventData.isPresent = true;
                _eatDBContext.EventAttendees.Attach(eventData);
                var entry = _eatDBContext.Entry(eventData);
                entry.Property(e => e.isPresent).IsModified = true;
                _eatDBContext.SaveChanges();

                return true;
            }
            catch ( Exception ex )
            {
                Debug.Write(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// To decrypt the cipher QR string so that we can get event and student ID's.
        /// </summary>
        /// <param name="cipherString"></param>
        /// <returns></returns>
        private static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("AIHYDCOMCPU2016"));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// To Split the string so that we get event id and student id.
        /// </summary>
        /// <param name="qRString"></param>
        /// <returns></returns>
        private static string[] UnpackQRString(string qRString)
        {
            return qRString.Split('@');
        }
    }
}