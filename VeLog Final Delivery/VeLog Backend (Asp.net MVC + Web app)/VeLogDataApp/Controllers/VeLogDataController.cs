using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VeLogDataApp.Models;
using System.Web.Security;

namespace VeLogDataApp.Controllers
{
    public class VeLogDataController : Controller
    {
        
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userRec)
        {
             
            VelogDataContext dbContext = new VelogDataContext();
          

            bool Exist = dbContext.tblvelogUsers.Any(x => x.Username == userRec.Username && x.Password == userRec.Password && x.Role == "User");
             if(Exist)
            {

                FormsAuthentication.SetAuthCookie(userRec.Username, false);

               
               
                    return RedirectToAction("Create", "VeLogData");
               
            }
           
            
                return View(userRec);
            
        }


        public ActionResult CreateNewLoaction()
        {
            
            VelogDataContext dbContext = new VelogDataContext();
            
            ViewBag.Division = new SelectList(dbContext.tblvelogDivisions, "Division", "Division");
            ViewBag.Course = new SelectList(dbContext.tblvelogCourses, "Courses", "Courses");
            ViewBag.Campus = new SelectList(dbContext.tblvelogCampus, "Campus", "Campus");
           
            return PartialView("~/Views/Shared/EditorTemplates/AddLocation.cshtml");

        }

        [Authorize]
        public ActionResult Create()

        {
            VelogDataContext dbContext = new VelogDataContext();

            ViewBag.Division = new SelectList(dbContext.tblvelogDivisions, "Division", "Division");
            ViewBag.Course = new SelectList(dbContext.tblvelogCourses, "Courses", "Courses");
            ViewBag.Campus = new SelectList(dbContext.tblvelogCampus, "Campus", "Campus");
            ViewBag.VehicleRego = new SelectList(dbContext.tblVelogCars, "Registration", "Registration");
            
            return View();
        }

        
        public JsonResult GetMakeModel(String Reg)
        {
            List<CarSpec> list = new List<CarSpec>();
            VelogDataContext dbContext = new VelogDataContext();
            tblVelogCar rec = dbContext.tblVelogCars.First(x => x.Registration == Reg);

            list.Add(new CarSpec{ make = rec.Make, model = rec.Model, color = rec.Colour });
           
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEndMileage(String Reg)
        {
            List<CarMileage> list = new List<CarMileage>();
            VelogDataContext dbContext = new VelogDataContext();

            var testlist = dbContext.VeLogDatas.Where(p => p.VehicleRego == Reg);

            testlist = testlist.OrderByDescending(p => p.Id);

            var rec = testlist.FirstOrDefault();

            if(rec != null)
                list.Add(new CarMileage { startMileage =  rec.StartMileage, endMileage = rec.EndMileage, rego = rec.VehicleRego });
            else
                list.Add(new CarMileage { startMileage = 0, endMileage =0, rego = "xxx" });

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCourses(String Division)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            VelogDataContext dbContext = new VelogDataContext();

            list = dbContext.tblvelogCourses.Where(d=>d.Division== Division).Select(c => new SelectListItem
            {
                Value = c.Courses,
                Text = c.Courses

            }).ToList();
        return Json(new SelectList(list, "Value", "Text"), JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult Create(VeLogDataView VeLogDataRec)
        {
             
            VelogDataContext dbContext = new VelogDataContext();

             int maxId;
            VeLogDataClient VC = new VeLogDataClient();



            if (dbContext.VeLogDatas.Any()) 

            {
                maxId  = dbContext.VeLogDatas.Max(item => item.Id);
            }
            else
            {
               maxId = 0;
            }

            if (ModelState.IsValid)
            {


                Double MileageAvg = VeLogDataRec.EndMileage - VeLogDataRec.StartMileage;
                if (VeLogDataRec.LocationList != null)
                {
                    MileageAvg = MileageAvg / (VeLogDataRec.LocationList.Count()+1);
                }

                VeLogData DataToSubmit = new VeLogData();

                DataToSubmit.Comment = VeLogDataRec.Comment;

                DataToSubmit.CreationDate = DateTime.Now; ;
                DataToSubmit.Distance = MileageAvg;

                DataToSubmit.DriverName = VeLogDataRec.DriverName;
                DataToSubmit.EndMileage = VeLogDataRec.EndMileage;
                DataToSubmit.StartMileage = VeLogDataRec.StartMileage;
                DataToSubmit.VehicleRego = VeLogDataRec.VehicleRego;
                DataToSubmit.TripId = VeLogDataRec.VehicleRego + "--" + maxId;
                DataToSubmit.Campus = VeLogDataRec.Campus;
                DataToSubmit.Division = VeLogDataRec.Division;
                DataToSubmit.Course = VeLogDataRec.Course;

                // VC.Create(DataToSubmit);
                dbContext.VeLogDatas.Add(DataToSubmit);
                dbContext.SaveChanges();

                if (VeLogDataRec.LocationList != null)
                {

                    for (int i = 0; i < VeLogDataRec.LocationList.Count(); i++)
                    {


                        VeLogData DataToSubmit1 = new VeLogData();

                        DataToSubmit1.Comment = VeLogDataRec.Comment;

                        DataToSubmit1.CreationDate = DateTime.Now; ;
                        DataToSubmit1.Distance = MileageAvg;

                        DataToSubmit1.DriverName = VeLogDataRec.DriverName;
                        DataToSubmit1.EndMileage = VeLogDataRec.EndMileage;
                        DataToSubmit1.StartMileage = VeLogDataRec.StartMileage;
                        DataToSubmit1.VehicleRego = VeLogDataRec.VehicleRego;
                        DataToSubmit1.TripId = VeLogDataRec.VehicleRego + "--" + maxId;
                        DataToSubmit1.Campus = VeLogDataRec.LocationList[i].Campus;
                        DataToSubmit1.Division = VeLogDataRec.LocationList[i].Division;
                        DataToSubmit1.Course = VeLogDataRec.LocationList[i].Course;
                        dbContext.VeLogDatas.Add(DataToSubmit1);
                        dbContext.SaveChanges();
                        //VC.Create(DataToSubmit1);

                    }

                }
                return RedirectToAction("Create");
            }

            else
            {

                ViewBag.Division = new SelectList(dbContext.tblvelogDivisions, "Division", "Division");
                ViewBag.Course = new SelectList(dbContext.tblvelogCourses, "Courses", "Courses");
                ViewBag.Campus = new SelectList(dbContext.tblvelogCampus, "Campus", "Campus");
                ViewBag.VehicleRego = new SelectList(dbContext.tblVelogCars, "Registration", "Registration");


                if (VeLogDataRec.VehicleRego != null)
                {
                    tblVelogCar rec = dbContext.tblVelogCars.First(x => x.Registration == VeLogDataRec.VehicleRego);
                    VeLogDataRec.MakeCmb = rec.Model + "--" + rec.Make + "--" + rec.Colour;
                }
               
              
                return View("Create", VeLogDataRec);
            }
           



        }
    }
}