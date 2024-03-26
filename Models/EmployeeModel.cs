using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement_CIS.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmailID { get; set; }
        public string EmployeeDOB { get; set; }
        public int EmployeeExperienceLevel_ID { get; set; }
        public string EmployeeExperienceLevel { get; set; }
        public int EmployeeGender_ID { get; set; }
        public string EmployeeGender { get; set; }
        public string EmployeeAddress { get; set; }
        public int TotalRecords { get; set; }
    }
}