using Microsoft.AspNet.Identity;
using HostelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HostelSystem.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult MarkAttendance(FormCollection atd)
        {
            try
            {
                string[] a = atd.AllKeys;
                var added = atd[a[0]].Split(',');
                HostelManagementSystemEntities db = new HostelManagementSystemEntities();
                int i = 0;
                MessAttandance M = new MessAttandance();
                foreach (MarkAttendance m in Attendance.lst)
                {
                    M.RoomNo = m.RoomNo;
                    M.FoodId = m.FoodId;
                    M.StudentId = m.StudentId;
                    M.DateMarked = m.DateMarked;
                    M.AtdCount = Convert.ToInt32(added[i]);
                    db.MessAttandances.Add(M);
                    db.SaveChanges();
                    i++;
                }
                return View(Attendance.lst);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [HttpGet]
        public ActionResult MarkAttendance()
        {
            try
            {
                AddAllToStaticList();
                return View(Attendance.lst);

            }
            catch (Exception e)
            {
                throw (e);
            }

        }


        [NonAction]
        public void MakeStaticListEmpty()
        {
            foreach (MarkAttendance m in Attendance.lst)
            {
                Attendance.lst.Remove(m);
            }
        }

        
        [NonAction]
        public List<MarkAttendance> AddStudentsFromDatabase()
        {
            //Add students from db to student list
            List<MarkAttendance> lstStudents = new List<MarkAttendance>();
            HostelManagementSystemEntities db = new HostelManagementSystemEntities();
            foreach (StudentDb s in db.StudentDbs)
            {
                MarkAttendance m = new MarkAttendance();
                m.StudentId = s.CNIC;
                m.regNo = Convert.ToString(s.RegNo);
                m.name = s.Name;
                lstStudents.Add(m);
            }
            return lstStudents;
        }




        [NonAction]
        public List<MarkAttendance> AddFoodFromDatabase()
        {
            // Add food from db to food list
            HostelManagementSystemEntities db = new HostelManagementSystemEntities();
            string type;
            DateTime dt = DateTime.Now;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = new TimeSpan(12, 30, 00);
            TimeSpan end = new TimeSpan(1, 30, 00);
            if (now >= start && now <= end)
            {
                type = "lunch";
            }
            else
            {
                type = "Dinner";
            }
            List<MarkAttendance> lstfood = new List<MarkAttendance>();
            foreach (Fooditem f in db.Fooditems)
            {
                MarkAttendance m = new MarkAttendance();
                if (f.Dday == dt.DayOfWeek.ToString() && f.FoodType == type)
                {
                    m.FoodId = f.FoodId;
                    m.FoodName = f.FoodName;
                    lstfood.Add(m);
                }
            }
            return lstfood;
        }


        [NonAction]
        public List<MarkAttendance> AddAllToStaticList()
        {
            MakeStaticListEmpty();
            HostelManagementSystemEntities db = new HostelManagementSystemEntities();
            List<MarkAttendance> lstStudents = AddStudentsFromDatabase();
            List<MarkAttendance> lstfood = AddFoodFromDatabase();

            int sCount = 0;
            int roomId = 1;

            List<int> lstInt = new List<int>();
            foreach (MarkAttendance m in lstStudents)
            {
                bool flag = false;
                MarkAttendance M = new MarkAttendance();
                M.StudentId = m.StudentId;
                M.regNo = db.StudentDbs.Find(m.StudentId).RegNo;
                M.name = db.StudentDbs.Find(m.StudentId).Name;
                M.RoomNo = db.RoomsDbs.Find(roomId).RoomNo;

                foreach (MarkAttendance m2 in lstfood)
                {
                    M.FoodId = m2.FoodId;
                    M.FoodName = m2.FoodName;
                }

                M.DateMarked = DateTime.Now;
                M.AtdCount = 0;
                Attendance.lst.Add(M);
                M.FoodId = M.FoodId;
                sCount++;
                if (sCount % 3 == 0)
                {
                    flag = true;
                }
                if (flag == true)
                {
                    roomId++;
                }
            }
            return Attendance.lst;
        }

        public List<int> Multiple(int num)
        {
            List<int> lst = new List<int>();
            int k = 3;
            int j = 1;
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    k = k * j;
                    j++;
                    lst.Add(k);
                }
                return lst;
            }
            return lst;
            
        }

        [NonAction]
        public bool MultipleOfThree(int num)
        {
            if (num % 3 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}