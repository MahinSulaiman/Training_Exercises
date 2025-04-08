using ClaysysLearningPortal.DAL;
using ClaysysLearningPortal.Error;
using ClaysysLearningPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Security.Claims;

namespace ClaysysLearningPortal.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly CoursesDAL _coursesDAL;
        private readonly UserDAL _userDAL;
        private readonly ErrorLogger _errorLogger;

        public StudentController(CoursesDAL dal,UserDAL userDAL,ErrorLogger errorLogger)
        {
            this._coursesDAL = dal;
            this._userDAL = userDAL;
            this._errorLogger = errorLogger;
        }
        public IActionResult Index()
        {
            try
            {
                List<Categories> categories = _coursesDAL.GetCategories();
                ViewData["Categories"] = new SelectList(categories, "CategoryId", "Category");
                return View();
            }
            catch (Exception ex)
            {
                 TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }
        }

        public IActionResult Courses(Guid? categoryId)
        {
            try
            {
                List<Courses> courseList = _coursesDAL.GetCoursesByCategory(categoryId);
                return Json(courseList);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }
        }

        public IActionResult Details(Guid courseId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim);
                    Courses course = _coursesDAL.GetCourseDetails(userId, courseId);

                    string base64Image = Convert.ToBase64String(course.CourseImage);
                    ViewBag.Base64Image = base64Image;

                    return View(course);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }
            
        }

        public IActionResult EnrollCourse(Guid courseId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim);
                    bool result = _coursesDAL.EnrollCourse(userId, courseId);

                    if (result)
                    {
                        return RedirectToAction("Index", "Student");
                    }

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();

            }
        }

        public IActionResult EnrolledCourses()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim);
                    List<Courses> courseList = _coursesDAL.GetEnrolledCourses(userId);

                    return View(courseList);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }

        }

        public IActionResult UserDetails()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim);
                    Users user = _userDAL.GetUserById(userId);
                    return View(user);

                }
                else
                {
                    return RedirectToAction("Login", "User");
                }

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }
        }

        public IActionResult EditUser(int userId)
        {
            try
            {
                Users user = _userDAL.GetUserById(userId);
                return View(user);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditUser(Users user)
        {
            try
            {
                bool result = _userDAL.UpdateUser(user);
                if (result)
                {
                    return RedirectToAction("UserDetails", "Student");
                }
                return View(user);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View(); 
            }
        }


    }
}
