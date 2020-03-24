using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_BAL.util;
using Event_Attendees_Tracker_DAL.DBOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Event_Attendees_Tracker_BAL.Tests
{
    [TestClass]
    public class TestEventRegistration
    {
        [TestMethod]
        public void TestInsertTblRegisteredStudents()
        {
            //Assign
            var tempDict = new Dictionary<string, string>
            {
                { "karthik", "Invalid Data" },
                {"TempData","TempData" },
                {"Tempdata2","tempData2" }
            };
            var emailList = new List<string>
            {
                {"Karthik@gmail.com" }
            };
            var moqMailSend = new Mock<IMailSend>();
            var moqEventRegistration = new Mock<IEventRegistrationDAL>();
            moqEventRegistration.Setup(x => x.InsertTblRegisteredStudents(It.IsAny<DataTable>()))
                .Returns(() => tempDict);
            moqMailSend.Setup(x => x.SendRegistrationMail(It.IsAny<List<string>>(), It.IsAny<int>()))
                .Returns(() => true);
            moqEventRegistration.Setup(x => x.InsertTblEventAttendees(It.IsAny<List<string>>(), It.IsAny<int>()))
                .Returns(() => emailList);
            EventRegistration eventRegistration=new EventRegistration(moqMailSend.Object,moqEventRegistration.Object);
            //Act
            var returnDict =
                eventRegistration.InsertTblRegisteredStudents(It.IsAny<DataTable>(), It.IsAny<int>(),
                    It.IsAny<string>());

            //Assert

            Assert.AreEqual(returnDict.Count,1);
            moqMailSend.Verify();
            moqEventRegistration.Verify();

        }
        
    }
}
