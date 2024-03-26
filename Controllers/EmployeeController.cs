using EmployeeManagement_CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement_CIS.DataAccessLayer;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Web.Services.Description;

namespace EmployeeManagement_CIS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeDAL dal = new EmployeeDAL();
           
        public ActionResult Index()
        {
            List<EmployeeModel> experienceLevels =  dal.GetExperienceLevel();
            ViewBag.ExperienceLevels = new SelectList(experienceLevels, "EmployeeExperienceLevel_ID", "EmployeeExperienceLevel");

            List<EmployeeModel> gender = dal.GetGender();
            ViewBag.Gender = new SelectList(gender, "EmployeeGender_ID", "EmployeeGender");

            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeeDetails(int offset, int PageSize, string SearchKeyword)
        {
            try
            {
                DataTable dtEmployee = new DataTable();
                dtEmployee = dal.GetEmployeeDetails(offset, PageSize, SearchKeyword);
                List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
                foreach (DataRow dr in dtEmployee.Rows)
                {
                    lstEmployee.Add(new EmployeeModel
                    {
                        EmployeeID = Convert.ToInt32(dr["ED_ID"]),
                        EmployeeName = dr["ED_NAME"].ToString(),
                        EmployeeEmailID = dr["ED_EMAIL"].ToString(),
                        EmployeeDOB = dr["ED_DOB"].ToString(),
                        EmployeeExperienceLevel_ID = Convert.ToInt32(dr["ED_ELM_EXP_LEVEL_ID"]),
                        EmployeeExperienceLevel = dr["ELM_LEVEL_NAME"].ToString(),
                        EmployeeGender_ID = Convert.ToInt32(dr["ED_MG_GENDER_ID"]),
                        EmployeeGender = dr["MG_GENDER_NAME"].ToString(),
                        EmployeeAddress = dr["ED_ADDRESS"].ToString(),
                        TotalRecords = Convert.ToInt32(dr["TotalRecords"])
                    });

                }
                return Json(new { success = true, data = lstEmployee }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsertUpdateEmployeeDetails(EmployeeModel employee)
        {
            try
            {
                DataTable dtSave = new DataTable();
                dtSave = dal.InsertUpdateEmployeeDetails(employee.EmployeeID,employee.EmployeeName, employee.EmployeeEmailID, employee.EmployeeDOB, 
                    employee.EmployeeExperienceLevel_ID,employee.EmployeeGender_ID, employee.EmployeeAddress);
                List<EmployeeModel> lstEmployee = new List<EmployeeModel>();

                string message = dtSave.Rows[0]["MESSAGE"].ToString();
                int messageCode = Convert.ToInt16(dtSave.Rows[0]["MESSAGECODE"]);

                return Json(new { success = true, data = message, code = messageCode }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}