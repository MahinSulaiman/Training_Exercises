using ClaysysLearningPortal.DAL;
using ClaysysLearningPortal.Error;
using ClaysysLearningPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClaysysLearningPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDAL _userDAL;
        private readonly ErrorLogger _errorLogger;
        public UserController(UserDAL dal,ErrorLogger errorLogger)
        {
            _userDAL = dal;
            _errorLogger = errorLogger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "model data is invalid";
                }
                bool result = _userDAL.AddUser(user);
                if (!result)
                {
                    TempData["errorMessage"] = "user not registered";
                }
                TempData["SuccessMessage"] = "User added successfully";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View(user);

            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username,string password)
        {
            try
            {
                UserLogin loginUser = _userDAL.Login(username, password);

                if (loginUser.Role != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,loginUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, loginUser.Role)
            };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    if (loginUser.Role == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Student");
                }
                else 
                {
                    TempData["ErrorMessage"] = "Invalid username or password";
                    throw new Exception("Invalid username or password");
                    //return View();
                }
                 
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                _errorLogger.WriteError(ex.Message);
                return View();

            }
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
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
