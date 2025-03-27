using ClaysysLearningPortal.DAL;
using ClaysysLearningPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ClaysysLearningPortal.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserDAL _userDAL;
        private readonly CoursesDAL _coursesDAL;
        public AdminController(UserDAL userDAL,CoursesDAL coursesDAL)
        {
            _userDAL = userDAL;
            _coursesDAL = coursesDAL;
        }
        public IActionResult Index()
        {
            try
            {
                List<Courses> courseList = _coursesDAL.GetAllCourses();
                return View(courseList);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult CourseDetails(Guid courseId)
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
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "model data is invalid";
                    return View();
                }
                bool result = _userDAL.AddUser(user);
                if (!result)
                {
                    TempData["errorMessage"] = "user not registered";
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        public IActionResult Users()
        {
            try
            {
                List<Users> userList = _userDAL.GetAllUsers();
                return View(userList);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
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
                    return RedirectToAction("Users", "Admin");
                }
                return View(user);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        public IActionResult DeleteUser(int userId)
        {
            try
            {
                Users user = _userDAL.GetUserById(userId);
                return View(user);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUserConfirm(int userId)
        {
            try
            {
                bool result = _userDAL.DeleteUser(userId);

                if (!result)
                {
                    TempData["ErrorMessage"] = "delete failed";
                    return View();
                }
                TempData["SuccessMessage"] = "deleted";
                return RedirectToAction("Users", "Admin");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult CreateCourse()
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
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreateCourse(Courses course,IFormFile courseImage) 
        {
            try
            {
                using (var memorystream = new MemoryStream())
                {
                    courseImage.CopyTo(memorystream);
                    course.CourseImage = memorystream.ToArray();
                }
                bool result = _coursesDAL.AddCourse(course);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Failed to update the course. Please check your data.\"";
                    return View();
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult GetEnrollRequest()
        {
            try
            {
                List<EnrolDetails> enrolList = _coursesDAL.GetEnrolRequest();
                return View(enrolList);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult UpdateEnroll(int userId,Guid courseId,string status)
        {
            try
            {
                bool result = _coursesDAL.UpdateEnroll(userId, courseId, status);
                if (result)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult DeleteCourse(Guid courseId)
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
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            } 
        }

        [HttpPost, ActionName("DeleteCourse")]
        public IActionResult DeleteCourseConfirm(Guid courseId)
        {
            try
            {
                bool result = _coursesDAL.DeleteCourse(courseId);
                if (!result)
                {
                    TempData["ErrorMessage"] = "failed";
                    return View();
                }
                return RedirectToAction("Index", "Admin");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult EditCourse(Guid courseId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    List<Categories> categories = _coursesDAL.GetCategories();
                    ViewData["Categories"] = new SelectList(categories, "CategoryId", "Category");

                    int userId = int.Parse(userIdClaim);
                    Courses course = _coursesDAL.GetCourseDetails(userId, courseId);

                    string base64Image = Convert.ToBase64String(course.CourseImage);
                    ViewBag.Base64Image = base64Image;

                    return View(course);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditCourse(Courses course, IFormFile courseImage)
        {
            try
            {
                using (var memorystream = new MemoryStream())
                {
                    courseImage.CopyTo(memorystream);
                    course.CourseImage = memorystream.ToArray();
                }
                bool result = _coursesDAL.EditCourse(course);
                if (!result)
                {
                    TempData["errorMessage"] = "Error while updating";
                    return View();
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
