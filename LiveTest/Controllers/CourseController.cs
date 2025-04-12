using LiveTest.DAL;
using LiveTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveTest.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseDAL _dal;
        public CourseController(CourseDAL dal)
        {
            _dal = dal;
        }

        // GET: CourseController
        public ActionResult Index()
        {
            List<Courses> courseList = new List<Courses>();
            try
            {
                courseList= _dal.GetAllCourses();
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(courseList);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            Courses course = new Courses();
            try
            {
               course = _dal.GetCourseById(id);
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Courses course)
        {
            //int result = 0;
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "model state not valid";
                    return View(course);
                }
                bool result = _dal.CreateCourse(course);
                if (!result)
                {
                    TempData["errorMessage"] = "course not created";
                    return View(course);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            Courses course = new Courses();
            try
            {
                course = _dal.GetCourseById(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Courses course)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "model not valid";
                    return View(course);
                }

                bool result = _dal.UpdateCourse(course);
                if (!result)
                {
                    TempData["errorMessage"] = "not updated";
                    return View(course);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(course);
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {

            Courses course = new Courses();
            try
            {
                course = _dal.GetCourseById(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                bool result = _dal.DeleteCourse(id);
                if (!result)
                {
                    TempData["errorMessage"] = "not deleted";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
