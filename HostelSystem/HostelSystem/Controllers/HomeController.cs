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
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDb login)
        {

            using (HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7())
            {

                var user = db.LoginDbs.SingleOrDefault(x => x.Email == login.Email);
                if (user != null)
                {
                    if(user.Status == "Yes")
                    {
                        if (user.Password == login.Password)
                        {
                            if (user.Designation == "Student")
                            {
                                return RedirectToAction("Registration");
                            }
                            if (user.Designation == "RT")
                            {
                                return RedirectToAction("EmployeeRegistration");
                            }
                            if (user.Designation == "Warden")
                            {
                                return RedirectToAction("EmployeeRegistration");
                            }
                            if (user.Designation == "SeniorWarden")
                            {
                                return RedirectToAction("Registration");
                            }
                            if (user.Designation == "MessEmployee")
                            {
                                return RedirectToAction("Registration");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "Email Or Password is InCorrect");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "can,t login before approval of the request");

                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Register yourself First");
                }


            }
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Registration(StudentRegistrationViewModel reg)
        {
            try
            {
                bool flag = false;
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
                foreach (StudentDb p in db.StudentDbs)
                {
                    if (p.Email == reg.Email)
                    {
                        flag = true;
                        ModelState.AddModelError("", "Email already exist");
                    }
                    if (p.CNIC == reg.CNIC)
                    {
                        flag = true;
                        ModelState.AddModelError("", "CNIC already exist");
                    }
                    if (p.RegNo == reg.RegNo)
                    {
                        flag = true;
                        ModelState.AddModelError("", "Invalid Registration Number");
                    }
                    if (flag == true)
                    {
                        break;
                    }
                }
                if (flag == true)
                {
                    return View();
                }
                StudentDb s = new StudentDb();
                s.Address = reg.Address;
                s.BloodGroup = reg.BloodGroup;
                s.CNIC = reg.CNIC;
                s.Contact_ = reg.ContactNo;
                s.FatherName = reg.FatherName;
                s.Name = reg.Name;
                s.DOB = reg.DOB;
                s.RegNo = reg.RegNo;
                s.Password = reg.password;
                s.Email = reg.Email;
                LoginDb l = new LoginDb();
                l.Email = reg.Email;
                l.Password = reg.password;
                l.Designation = "Student";
                l.Status = "NO";
                db.LoginDbs.Add(l);
                db.StudentDbs.Add(s);
                db.SaveChanges();
                ModelState.Clear();


                return RedirectToAction("login");
            }
            catch (Exception e)
            {
                throw (e);
            }



        }




        public ActionResult EmployeeRegistration()
        {
            return View();
        }

        [HttpPost]

        public ActionResult EmployeeRegistration(EmployeeRegistrationViewModel reg)
        {

            try
                
            {
                bool flag = false;
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
                EmployeeDb s = new EmployeeDb();

                foreach (EmployeeDb e in db.EmployeeDbs)
                {
                    if (e.Email == reg.email )
                    {
                        flag = true;
                        ModelState.AddModelError("", "Email already exist");
                        if(e.CNIC == reg.CNIC)
                        {
                            flag = true;
                            ModelState.AddModelError("", "CNIC already exist");
                            break;
                        }
                        break;
                    }
                    if(e.CNIC == reg.CNIC)
                    {
                        flag = true;
                        ModelState.AddModelError("", "CNIC already exist");
                        break;
                    }
                }
                if (flag == true)
                {
                    return View();
                }
                else
                {
                    s.Address = reg.Address;
                    s.BloodGroup = reg.BloodGroup;
                    s.CNIC = reg.CNIC;
                    s.ContactNo = reg.ContactNo;
                    s.FatherName = reg.FatherName;
                    s.Name = reg.Name;
                    s.DOB = reg.DOB;
                    s.Password = reg.password;
                    s.Email = reg.email;
                    

                    s.Designation = reg.Designation;

                    LoginDb l = new LoginDb();

                    l.Designation = reg.Designation;
                    l.Email = reg.email;
                    l.Password = reg.password;
                    l.Status = "NO";
                    db.LoginDbs.Add(l);
                    db.EmployeeDbs.Add(s);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "You are Registered Successfully";

                    return RedirectToAction("login");
                }
                
            }
                


                
            
            catch (Exception e)
            {
                throw (e);
            }
        }


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
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
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
                return RedirectToAction("WardenrequestApproal", "Employee");
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
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
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
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
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
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
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