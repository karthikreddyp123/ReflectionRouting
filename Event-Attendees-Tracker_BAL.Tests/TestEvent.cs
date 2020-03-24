using System;
using System.Collections.Generic;
using Event_Attendees_Tracker_BAL.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Event_Attendees_Tracker_BAL.User_Actions;
using System.Data;
using System.Runtime.Remoting.Messaging;
using Event_Attendees_Tracker_DAL;
using Event_Attendees_Tracker_DAL.DBQueries;
using Event_Attendees_Tracker_DAL.Models;

namespace Event_Attendees_Tracker_BAL.Tests
{
    [TestClass]
    public class TestEvent
    {
        [TestMethod]
        public void TestAddEvent()
        {
            //Assign
            var tempDict = new Dictionary<string, string>
            {
                { "Karthik", "Karthik" },
                {"TempData","TempData" },
                {"Tempdata2","tempData2" }
            };
            var moqEventQuery = new Mock<IEventQuery>();
            var moqEventRegistration = new Mock<IEventRegistration>();
            var moqFetchActiveEvents = new Mock<IFetchActiveEvents>();
            moqEventRegistration.Setup(x =>
                x.InsertTblRegisteredStudents(It.IsAny<DataTable>(), It.IsAny<int>(), It.IsAny<string>())).Returns(() => tempDict);
            moqEventQuery.Setup(x => x.AddEvent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>(), It.IsAny<DateTime>(),
                    It.IsAny<int>()))
                .Returns(() => true);
            Events events = new Events(moqEventRegistration.Object, moqEventQuery.Object,moqFetchActiveEvents.Object);
            //Act
            var ret = events.AddEvent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>(), It.IsAny<DateTime>(), It.IsAny<DataTable>(), It.IsAny<int>());
            //Assert
            Assert.AreEqual(ret.Count, 3);
        }

        [TestMethod]
        public void TestFetchActiveEvents()
        {
            //Assign
            var returnList = new List<EventDetails>();
            returnList.Add(new EventDetails()
            {
                CanRegister = true,
                CreatedAt = DateTime.Now,
                CreatedBy = 1,
                Description = "description",
                EndTime = new TimeSpan(20,10,10,10),
                EventDate = DateTime.Now,
                EventName = "CodeInject",
                ID=1,
                isActive = true,
                PosterImage = "somepath",
                UpdatedBy = 1,
                StartTime = new TimeSpan(20, 10, 10, 10),
                UpdatedAt = DateTime.Now,
                Venue = "Hyderabad"
            });
            var moqEventQuery = new Mock<IEventQuery>();
            var moqEventRegistration = new Mock<IEventRegistration>();
            var moqFetchActiveEvents = new Mock<IFetchActiveEvents>();
            moqFetchActiveEvents.Setup(x => x.GetActiveEvents(It.IsAny<int>())).Returns(() => returnList);
            Events events=new Events(moqEventRegistration.Object,moqEventQuery.Object,moqFetchActiveEvents.Object);
            //Act
            var eventList = events.fetchActiveEvents(It.IsAny<int>());
            //Assert
            Assert.AreEqual(eventList.Count,1);

        }
    }
}
