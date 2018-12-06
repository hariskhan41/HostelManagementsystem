using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HostelSystem.Models
{
    public class multitables
    {
        public IEnumerable<StudentRegistrationViewModel> s { set; get; }
        public IEnumerable<EmployeeRegistrationViewModel> emp { set; get; }
    }
}