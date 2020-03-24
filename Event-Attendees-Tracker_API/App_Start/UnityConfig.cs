using Event_Attendees_Tracker_BAL.Authentication;
using Event_Attendees_Tracker_BAL.ServiceActions;
using Event_Attendees_Tracker_BAL.User_Actions;
using Event_Attendees_Tracker_BAL.util;
using System.Web.Http;
using Event_Attendees_Tracker_DAL.App_Start;
using Unity;
using Unity.WebApi;

namespace Event_Attendees_Tracker_API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUserLogin, UserLogin>();
            container.RegisterType<IEvents, Events>();
            container.RegisterType<IEncryptDecrypt, EncryptDecrypt>();
            container.RegisterType<IMailSend, MailSend>();
            container.RegisterType<IEventRegistration, EventRegistration>();
            container.RegisterType<ITryScanEvent, TryScanEvent>();
            container.RegisterType<IMailer, Mailer>();
            container.RegisterType<IMarkAttendance, MarkAttendance>();
            container.AddNewExtension<CustomUnityConfig>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}