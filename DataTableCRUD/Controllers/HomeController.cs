using DataTableCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataTableCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetEmployees()
        {
            using (MyDatabaseEntities et = new MyDatabaseEntities())
            {
                var employees = et.Employees.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities et = new MyDatabaseEntities())
            {
                var emp = et.Employees.Where(r => r.EmployeeId == id).FirstOrDefault();
                return View(emp);
            }
        }

        [HttpPost]
        public ActionResult Save(Employee emp)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (MyDatabaseEntities et = new MyDatabaseEntities())
                {
                    if(emp.EmployeeId >0)
                    {
                        var v = et.Employees.Where(r => r.EmployeeId == emp.EmployeeId).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = emp.FirstName;
                            v.LastName = emp.LastName;
                            v.EmailID = emp.EmailID;
                            v.City = emp.City;
                            v.Country = emp.Country;
                        }
                        
                    }
                    else
                    {
                        et.Employees.Add(emp);
                    }

                    et.SaveChanges();
                    status = true;
                }
                

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities et = new MyDatabaseEntities())
            {
                var emp = et.Employees.Where(r => r.EmployeeId == id).FirstOrDefault();
                if (emp!=null)
                {
                    return View(emp); 
                }

                else
                {
                    return HttpNotFound(); 
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (MyDatabaseEntities et=new MyDatabaseEntities())
            {
                var emp = et.Employees.Where(r => r.EmployeeId == id).FirstOrDefault();
                if(emp!=null) 
                {
                    et.Employees.Remove(emp);
                    et.SaveChanges();
                    status = true;
                }
            }  

            return new JsonResult { Data = new { status = status } };
        }

    }
}