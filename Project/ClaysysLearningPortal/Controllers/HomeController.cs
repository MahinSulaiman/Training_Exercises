using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaysysLearningPortal.Models;
using ClaysysLearningPortal.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClaysysLearningPortal.Error;

namespace ClaysysLearningPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CoursesDAL _coursesDAL;

    public HomeController(ILogger<HomeController> logger, CoursesDAL dal)
    {
        _logger = logger;
        this._coursesDAL = dal;
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
            return View();
        }
    }


    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
