using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaysysLearningPortal.Models;
using ClaysysLearningPortal.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        List<Categories> categories = _coursesDAL.GetCategories();
        ViewData["Categories"] = new SelectList(categories, "CategoryId", "Category");
        return View();
    }

    public IActionResult Courses(Guid? categoryId)
    {
        List<Courses> courseList = _coursesDAL.GetCoursesByCategory(categoryId);
        return Json(courseList);
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
