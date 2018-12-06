using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HostelSystem.Models
{
    public class EmployeeRegistrationViewModel
    {

        public string CNIC { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public System.DateTime DOB { get; set; }
        public int ContactNo { get; set; }
        public string BloodGroup { get; set; }
        public string email { set; get; }
        public string status { set; get; }
    }

}