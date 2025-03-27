using CRUD_coreApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_coreApp.Models;

namespace CRUD_coreApp.Controllers
{
    public class EmployeeController : Controller
    {
        //EmployeeDAL _dal = new EmployeeDAL();
        private readonly EmployeeDAL _dal;
        public EmployeeController(EmployeeDAL dal)
        {
            _dal = dal;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> employees = _dal.GetEmployees();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int employeeId)
        {
            Employee employee = _dal.GetEmployeeById(employeeId);
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                bool result = _dal.InsertEmployee(employee);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Failed to insert";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int employeeId)
        {
            Employee employee = _dal.GetEmployeeById(employeeId);
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                bool result = _dal.UpdateEmployee(employee);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Failed to insert";
                    return View(employee);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(employee);
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int employeeId)
        {
            Employee employee = _dal.GetEmployeeById(employeeId);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int employeeId)
        {
            try
            {
                bool result = _dal.DeleteEmployee(employeeId);
                if (!result)
                {
                    TempData["ErrorMessage"] = "failed to delete";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] =ex.Message ;
                return View();
            }
        }
    }
}
