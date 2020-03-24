using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using Event_Attendees_Tracker_DAL.Database_Context;
using Event_Attendees_Tracker_DAL.DBQueries;
using Event_Attendees_Tracker_DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Event_Attendees_Tracker_DAL.Database_Context;
using System.Data;
using Event_Attendees_Tracker_DAL.DBOperations;

namespace Event_Attendees_Tracker_DAL.Tests
{
    [TestClass]
    public class UnitTestEventRegistrationDAL
    {
        [TestMethod]
        public void TestInsertTblRegisteredStudents()
        {
            DataTable dt=new DataTable();
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("ContactNumber");
            dt.Columns.Add("EmailID");
            dt.Columns.Add("StudentRollNo");
            dt.Columns.Add("CollegeName");
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1][0] = "Karthik";
            dt.Rows[dt.Rows.Count - 1][1] = "Patlolla";
            dt.Rows[dt.Rows.Count - 1][2] = "9133793161";
            dt.Rows[dt.Rows.Count - 1][3] = "karthik1224325@gmail.com";
            dt.Rows[dt.Rows.Count - 1][4] = "12234";
            dt.Rows[dt.Rows.Count - 1][5] = "IITH";

            //{
            //    CollegeDetails = null,
            //    ContactNumber = "9133793161",
            //    EmailID = "karthik@gmail.com",
            //    FirstName = "Karthik",
            //    LastName = "Patlolla",
            //    StudentRollNumber = "12234",
            //});

            var moqDBInstance = new Mock<EAT_DBContext>();
            var moqDBset = new Mock<DbSet<RegisteredStudents>>();
            var moqCheckStudents=new Mock<ICheckStudentExists>();
            moqDBInstance.Setup(m => m.RegisteredStudents).Returns(moqDBset.Object);
            moqCheckStudents.Setup(x => x.CheckStudent(It.IsAny<string>())).Returns(true);
            EventRegistrationDAL eventRegistrationDal=new EventRegistrationDAL(moqCheckStudents.Object,moqDBInstance.Object);
            var studentDict = eventRegistrationDal.InsertTblRegisteredStudents(dt);
            moqDBset.Verify(m=>m.Add(It.IsAny<RegisteredStudents>()),Times.Once);
            moqDBInstance.Verify(m=>m.SaveChanges(),Times.Once);
            Assert.AreEqual(studentDict.Count,1);
        }
    }
}
