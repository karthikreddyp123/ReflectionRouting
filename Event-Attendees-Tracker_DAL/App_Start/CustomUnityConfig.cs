using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.DBOperations;
using Event_Attendees_Tracker_DAL.DBQueries;
using Unity.Extension;
using Unity;

namespace Event_Attendees_Tracker_DAL.App_Start
{
    public class CustomUnityConfig : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IEventRegistrationDAL, EventRegistrationDAL>();
            Container.RegisterType<IEventQuery,EventQuery>();
            Container.RegisterType<IFetchActiveEvents, FetchActiveEvents>();
        }

    }
}
