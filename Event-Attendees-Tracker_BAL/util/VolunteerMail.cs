using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Event_Attendees_Tracker_BAL.util
{
    class VolunteerMail
    {
            [DllImport("wininet.dll")]
            public extern static bool InternetGetConnectedState(out int Description, int ReversedValue);

            public static void SendRegistrationMail(string EventName, string Venue, TimeSpan startTime, DateTime eventDate, string Recepient, string password)
            {
                //var eventDetails = FetchEvent.fetchEventData(EventID);
                int des;
                if (InternetGetConnectedState(out des, 0))
                {
                    Debug.Print(AppDomain.CurrentDomain.BaseDirectory);
                    string FilePath = HttpContext.Current.Server.MapPath("~/MailTemplate/Register.html");
                    StreamReader str = new StreamReader(FilePath);
                    string MailBody = str.ReadToEnd();
                    MailBody = MailBody.Replace("cid:Name", EventName);
                    MailBody = MailBody.Replace("cid:Date", eventDate.ToString());//Change Event Date
                    MailBody = MailBody.Replace("cid:Time", startTime.ToString());
                    MailBody = MailBody.Replace("cid:Venue", Venue);
                    MailBody = MailBody.Replace("cid:UserName", Recepient);
                    MailBody = MailBody.Replace("cid:Password", password);


                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("MAIL");
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mail.From = new MailAddress(Environment.UserName + "@epam.com");

                    mail.To.Add(Recepient);

                    mail.Subject = EventName + " Registration Mail";
                    mail.IsBodyHtml = true;
                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    string logoPath = HttpContext.Current.Server.MapPath("~/Images/EPAM_LOGO_WhiteAndBlue.png");
                    string wallpaper = HttpContext.Current.Server.MapPath("~/Images/EPAM_winter_party-081_72DPIRGB.jpg");
                    string ProjectLogo = HttpContext.Current.Server.MapPath("~/Images/ProjectLogo.png");
                    mail.AlternateViews.Add(ImageEmbed.imageEmbedder(logoPath, MailBody, "logo"));
                    mail.AlternateViews.Add(ImageEmbed.imageEmbedder(wallpaper, MailBody, "background"));
                    mail.AlternateViews.Add(ImageEmbed.imageEmbedder(ProjectLogo, MailBody, "ProjectLogo"));
                    try
                    {
                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }
            }
        }
    }

