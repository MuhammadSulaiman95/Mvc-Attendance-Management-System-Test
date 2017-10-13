using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcTestStudent;

namespace MvcTestStudent.Controllers
{
    public class StudentsController : Controller
    {
        private demoEntities db = new demoEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            ViewBag.students = students;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int[] checkes, DateTime attendanceDate)
        {
            
           StudentsAttendance sa = new StudentsAttendance();
            Student  s = new Student();            
            var chkDate = db.StudentsAttendances.Where(a => a.Date == attendanceDate).FirstOrDefault();
            if (chkDate== null)
            {
                foreach (var id in checkes)
                {
                    sa.StudentId = id;
                    sa.Date = attendanceDate;
                    db.StudentsAttendances.Add(sa);
                    db.SaveChanges();
                    TempData["message"] = "Attendance has been marked";
                }
            }
            else
            {
                TempData["message"] = "Attendance has already been marked ";
            }

            var students = db.Students.ToList();
            ViewBag.students = students;

            return View();
        }
      
    }
}
