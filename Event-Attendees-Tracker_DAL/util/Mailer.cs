using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace Event_Attendees_Tracker_DAL.util
{
    public static class Mailer
    {
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReversedValue);

        public static void SendQRMail(string destinationEmail, string eventName, int eventID, int studentID)
        {
            int des;
            if ( InternetGetConnectedState(out des, 0) )
            {
                Debug.Print(AppDomain.CurrentDomain.BaseDirectory);
                string FilePath = HttpContext.Current.Server.MapPath("~/MailTemplate/QRMailTemplate.html");
                StreamReader str = new StreamReader(FilePath);
                string MailBody = str.ReadToEnd();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("MAIL");
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.From = new MailAddress(Environment.UserName + "@epam.com");
                mail.To.Add(destinationEmail);
                mail.Subject = "QR Code for " + eventName;
                MailBody = MailBody.Replace("cid:name", eventName);
                QR.GenerateQR(Encrypt(studentID.ToString() + "@" + eventID.ToString()));
                mail.IsBodyHtml = true;
                SmtpServer.Port = 25;
                SmtpServer.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                string logoPath = HttpContext.Current.Server.MapPath("~/Images/EPAM_LOGO_WhiteAndBlue.png");
                string qrPath = HttpContext.Current.Server.MapPath("~/Images/qr.jpeg");
                string ProjectLogo = HttpContext.Current.Server.MapPath("~/Images/ProjectLogo.png");
                mail.AlternateViews.Add(imageEmbedder(logoPath, MailBody, "logo"));
                mail.AlternateViews.Add(imageEmbedder(ProjectLogo, MailBody, "ProjectLogo"));
                mail.AlternateViews.Add(imageEmbedder(logoPath, MailBody, "logo"));
                mail.AlternateViews.Add(imageEmbedder(qrPath, MailBody, "QR"));
                mail.AlternateViews.Add(imageEmbedder(ProjectLogo, MailBody, "ProjectLogo"));

                try
                {
                    SmtpServer.Send(mail);
                }
                catch ( Exception ex )
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        /// <summary>
        /// To embed images into html template.
        /// </summary>
        /// <param name="SourcePath"></param>
        /// <param name="MailBody"></param>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        private static AlternateView imageEmbedder(string SourcePath, string MailBody, string SourceId)
        {
            var htmlView = AlternateView.CreateAlternateViewFromString(MailBody, null, "text/html");
            var resource = new LinkedResource(SourcePath) { ContentId = SourceId };
            resource.TransferEncoding = TransferEncoding.Base64;
            htmlView.LinkedResources.Add(resource);
            return htmlView;
        }

        /// <summary>
        /// This is take a string and returns encrypted string.
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("AIHYDCOMCPU2016"));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}