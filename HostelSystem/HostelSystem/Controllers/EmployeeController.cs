using HostelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace HostelSystem.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult RTrequestApproval()
        {

            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            List<StudentRegistrationViewModel> log = new List<StudentRegistrationViewModel>();
            List<EmployeeRegistrationViewModel> emp = new List<EmployeeRegistrationViewModel>();
            foreach(LoginDb l in db.LoginDbs)
            {
                if (l.Status == "NO" && l.Designation == "Student")
                {
                    foreach (StudentDb d in db.StudentDbs)
                    {
                        if (l.Email == d.Email)
                        {
                            StudentRegistrationViewModel s = new StudentRegistrationViewModel();
                            s.Email = l.Email;
                            log.Add(s);


                        }
                    }
                }
                if (l.Status == "NO" && l.Designation == "Mess Employee")
                    {
                        foreach (EmployeeDb d in db.EmployeeDbs)
                        {
                            if (l.Email == d.Email)
                            {
                            EmployeeRegistrationViewModel e = new EmployeeRegistrationViewModel();
                                e.email = l.Email;
                                emp.Add(e);


                            }
                        }
                }
            }
            var model = new multitables();
            model.emp = emp.ToList();
            model.s = log.ToList();
            //HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            //List<LOGIN> lg = new List<LOGIN>();    
            //foreach(LoginDb l in db.LoginDbs)
            //{
            //    if(l.Status == "NO" && l.Designation== "Student")
            //    {
            //        LOGIN login = new LOGIN();
            //        login.Email = l.Email;
            //        lg.Add(login);
            //    }
            //}
            //return View(lg);
            return View(model);
        }

        public ActionResult DisApproveStudent(string id1)
        {
            try
            {
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
                foreach (StudentDb s in db.StudentDbs)
                {
                    if (id1 == s.Email)
                    {
                        SendMailForDisapprove(id1);
                        db.StudentDbs.Remove(s);
                       
                    }
                }
                db.SaveChanges();
                foreach (LoginDb l in db.LoginDbs)
                {
                    if (id1 == l.Email)
                    {
                        db.LoginDbs.Remove(l);
                      
                    }
                }
                db.SaveChanges();

                return RedirectToAction("RTrequestApproval", "Home");
            }
            catch(Exception e)
            {
                throw (e);
            }  
        }

      
        public ActionResult ApproveStudent(string mail)
        {
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            foreach(LoginDb l in db.LoginDbs)
            {
                if(l.Email == mail)
                {
                    SendMail(mail);
                    db.LoginDbs.Find(mail).Status = "Yes";
                }
            }
            db.SaveChanges();
           
            return RedirectToAction("RTrequestApproval", "Employee");
        }

        public ActionResult WardenrequestApproal()
        {
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            List<LOGIN> lg = new List<LOGIN>();
            foreach (LoginDb l in db.LoginDbs)
            {
                if (l.Status == "NO" && l.Designation == "RT")
                {
                    LOGIN login = new LOGIN();
                    login.Email = l.Email;
                    lg.Add(login);
                }
            }
            return View(lg);

        }

        public ActionResult DisApproveRT(string mail)
        {
            try
            {
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
                foreach (EmployeeDb s in db.EmployeeDbs)
                {
                    if (mail == s.Email)
                    {
                        SendMailForDisapprove(mail);
                        db.EmployeeDbs.Remove(s);
                    }
                }
                db.SaveChanges();
                foreach (LoginDb l in db.LoginDbs)
                {
                    if (mail == l.Email)
                    {
                        db.LoginDbs.Remove(l);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("WardenrequestApproal", "Employee");
            }
            catch (Exception e)
            {
                throw (e);
            }


        }



        


        
        public ActionResult ApproveRT(string mail)
        {
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            foreach (LoginDb l in db.LoginDbs)
            {
                if (l.Email == mail)
                {
                    SendMail(mail);
                    db.LoginDbs.Find(mail).Status = "Yes";
                }
            }
            db.SaveChanges();

            return RedirectToAction("WardenrequestApproal", "Employee");
        }

        public ActionResult SeniorWardenrequestApproval()
        {
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            List<LOGIN> lg = new List<LOGIN>();
            foreach (LoginDb l in db.LoginDbs)
            {
                if (l.Status == "NO" && l.Designation == "Warden")
                {
                    LOGIN login = new LOGIN();
                    login.Email = l.Email;
                    lg.Add(login);
                }
            }
            return View(lg);
        }

        public ActionResult DisApproveWarden(string id1)
        {
            try
            {
                HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
                foreach (EmployeeDb s in db.EmployeeDbs)
                {
                    if (id1 == s.Email)
                    {
                        SendMailForDisapprove(id1);
                        db.EmployeeDbs.Remove(s);
                    }
                }
                db.SaveChanges();
                foreach (LoginDb l in db.LoginDbs)
                {
                    if (id1 == l.Email)
                    {
                        db.LoginDbs.Remove(l);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("SeniorWardenrequestApproval", "Employee");
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public ActionResult ApproveWarden(string id1)
        {
            HostelManagementSystemEntities7 db = new HostelManagementSystemEntities7();
            foreach (LoginDb l in db.LoginDbs)
            {
                if (l.Email == id1)
                {
                    SendMail(id1);
                    db.LoginDbs.Find(id1).Status = "Yes";
                }
            }
            db.SaveChanges();
            return RedirectToAction("SeniorWardenrequestApproval", "Employee");
        }


        [NonAction]
        public void SendMail(string email)
        {
            string from1 = "hostelmanagementsystemuet@gmail.com";
            GmailClass g = new GmailClass();
            string email1 = g.Email;

            try
            {
                using (MailMessage mail = new MailMessage(from1, email))
                {

                    mail.Subject = "Registration Request";

                    mail.Body = "Congratulations! Your request has been accepted.You can now login to your account";

                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from1, "Harry#123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    ViewData["Message"] = "Your Request Has been Accepted You can now login... ";
                }
            }
            catch (Exception ex)
            {
                String msg = ex.Message;
            }
        }

        [NonAction]
        public void SendMailForDisapprove(string email)
        {
            string from1 = "hostelmanagementsystemuet@gmail.com";
            GmailClass g = new GmailClass();
            string email1 = g.Email;

            try
            {
                using (MailMessage mail = new MailMessage(from1, email))
                {
                    mail.Subject = "Registration Request";
                    mail.Body = "Your request has been disapproved due to security reasons.";
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from1, "Harry#123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    ViewData["Message"] = "Your Request Has been Accepted You can now login... ";
                }
            }
            catch (Exception ex)
            {
                String msg = ex.Message;
            }
        }
    }
}