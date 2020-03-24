using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using Event_Attendees_Tracker.Filters;
using Event_Attendees_Tracker.Middlewares;
using Event_Attendees_Tracker.Modals;
using Event_Attendees_Tracker.Modals.Response_Models;
using Event_Attendees_Tracker_CustomResponseModel;
using System.Collections.Generic;

namespace Event_Attendees_Tracker.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class OrganizerController : Controller
    {
        RestClient client = new RestClient("https://localhost:44360/");

        //GET: /Organizer/Organizer        
        public ActionResult Dashboard()
        {
            var requestActive = new RestRequest("api/User/FetchActiveEvents?userId=" + (int)Session["userId"]) { Method = Method.GET };
            var responseActiveEvent = client.Execute(requestActive);
            ViewData["EventsResponse"] = ActiveEvents.FromJson(responseActiveEvent.Content);

            return View();
        }

        //GET: Organizer/CreateEvent        
        public ActionResult CreateEvent()
        {
            //to get volunteer names from the api
            var request = new RestRequest("api/Organzier/GetVolunteerMails");
            request.Method = Method.GET;
            IRestResponse<VolunteerEmailList> response = client.Execute<VolunteerEmailList>(request);
            List<VolunteerEmailList> volunteerEmailList = JsonConvert.DeserializeObject<List<VolunteerEmailList>>(response.Content);
            List<string> volunteerMails = volunteerEmailList.ConvertAll<string>(x => x.ToString());
            //converting list of type VolunteerEmailList to string type
            for (int i = 0; i < volunteerEmailList.Count; i++)
            {
                volunteerMails[i] = volunteerEmailList[i].EmailID.ToString();
            }
            ViewBag.DropDownList = volunteerMails;
            ViewBag.Readonly = false;
            return View();
        }

        //POST: /Organizer/CreateEvent
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public ActionResult CreateEvent(EventModel responseEventModel)
        {
            var excelFilePath = "";
            var imageFilePath = "";

            //Save Excel File
            if (responseEventModel.excelFile.ContentLength > 0 && responseEventModel.excelFile.ContentType.Contains("spreadsheetml"))
            {
                //Excel File

                excelFilePath = System.Web.HttpContext.Current.Server.MapPath($@"~/StudentExcel/{DateTime.Now.ToFileTime()}{responseEventModel.excelFile.FileName}");
                responseEventModel.excelFile.SaveAs(excelFilePath);
            }

            //Save Poster Image
            if (responseEventModel.posterImage.ContentLength > 0 && responseEventModel.posterImage.ContentType.Contains("image"))
            {
                //Poster Image File
                //TODO:
                //Change it to the relative Path
                imageFilePath = System.Web.HttpContext.Current.Server.MapPath($@"~/PosterImage/{DateTime.Now.ToFileTime()}{responseEventModel.posterImage.FileName}");
                responseEventModel.posterImage.SaveAs(imageFilePath);
            }

            //Get the Datatable After Parsing
            var parsedDataTable = new ParseExcel().InsertTblRegisteredStudents(excelFilePath);


            //To Delete the file
            //if (System.IO.File.Exists(excelFilePath))
            //{
            //    System.IO.File.Delete(excelFilePath);
            //}


            //Request Config
            var request = new RestRequest("api/User/CreateEvent");
            request.Method = Method.POST;

            //Adding JSON Body

            //TODO:
            //Add Volunteer Reference

            var requestedData = new
            {
                Name = responseEventModel.name,
                Venue = responseEventModel.venue,
                Description = responseEventModel.description,
                EventDate = Convert.ToDateTime(responseEventModel.eventDate),
                StartTime = responseEventModel.startTime,
                EndTime = responseEventModel.endTime,
                PosterImagePath = imageFilePath,
                AttendeesDataTable = parsedDataTable,
                CreatedBy = Convert.ToInt32(Session["userId"])
            };

            request.AddJsonBody(JsonConvert.SerializeObject(requestedData, Formatting.Indented));

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Debug.Print(response.Content);

            }
            if (response.Content != null)
            {
                if (response.Content.Length > 2)
                {
                    Dictionary<string, string> stuDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                    ExcelInvalidRows.InvalidRows(excelFilePath, stuDictionary);
                    string FilePath = System.Web.HttpContext.Current.Server.MapPath($@"~/OutputExcel/");
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath + "LogExcel" + ".xlsx");
                    string fileName = "LogExcel" + ".xlsx";
                    //To Delete the file
                    if (System.IO.File.Exists(excelFilePath))
                    {
                        System.IO.File.Delete(excelFilePath);
                    }
                    //To Delete the file
                    if (System.IO.File.Exists(FilePath + "LogExcel" + ".xlsx"))
                    {
                        System.IO.File.Delete(FilePath + "LogExcel" + ".xlsx");
                    }
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else
                {
                    return Redirect("Dashboard");
                }

            }
            return View();

        }

        //GET: /Organizer/ModifyEvent
        [HttpGet]
        public ActionResult ModifyEvent()
        {
            return View();
        }

        public ActionResult GetDetailsOfEvent(int? EventId)
        {
            TempData["EventID"] = EventId;
            return View();
        }

        public ActionResult GetDetails()
        {
            string EventId = TempData["EventID"].ToString();
            var request = new RestRequest("api/Organzier/GetDetailsOfStudents?EventId=" + EventId);
            request.Method = Method.GET;
            IRestResponse<EventAttendeesModelList> response = client.Execute<EventAttendeesModelList>(request);
            List<EventAttendeesModelList> studentList = JsonConvert.DeserializeObject<List<EventAttendeesModelList>>(response.Content);
            return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Reports()
        {
            try
            {
                //todo: Add ViewBag UserId
                var userId = (int)Session["UserId"];
                ViewBag.userId = userId;
                var request = new RestRequest("api/Event/PastEventAttendees?userId=" + ViewBag.userId);
                request.Method = Method.GET;
                var response = client.Execute(request);
                ViewData["eventData"] = null;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PastEventResponseModel> pastEventList = JsonConvert.DeserializeObject<List<PastEventResponseModel>>(response.Content);
                    ViewData["eventData"] = pastEventList.ToArray();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    ViewData["NoPastEvents"] = "No Events";
                }
                else
                {
                    ViewData["errorReports"] = "Error In Displaying Reports";
                }
            }
            catch (Exception e)
            {
                ViewData["errorReports"] = e.Message;
            }

            return View();


        }
        /// <summary>
        /// An Organizer action of Adding a Volunteer.
        /// </summary>
        /// <returns>An Add volunteer View with create action</returns>
        public ActionResult AddVolunteer()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        ///<summary>Post Method of adding a Volunteer</summary>
        public ActionResult AddVolunteer(VolunteerModel responseVolunteerModel)
        {
            var request = new RestRequest("api/Organzier/AddVolunteer");
            request.Method = Method.POST;
            var requestedData = new
            {
                FirstName = responseVolunteerModel.FirstName,
                LastName = responseVolunteerModel.LastName,
                UserUID = responseVolunteerModel.UserUID,
                EmailID = responseVolunteerModel.EmailID,
            };
            request.AddJsonBody(JsonConvert.SerializeObject(requestedData, Formatting.Indented));
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Debug.Print(response.Content);

            }
            ViewData["VolunteerAdded"] = "Volunteer Added Successfully";
            return RedirectToAction("CreateEvent");
        }


    }
}