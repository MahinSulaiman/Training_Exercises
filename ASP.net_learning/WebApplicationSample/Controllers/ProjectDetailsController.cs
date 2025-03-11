using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationSample.DAL;
using WebApplicationSample.Models;

namespace WebApplicationSample.Controllers
{
    public class ProjectDetailsController : Controller
    {
        ProjectDetails_DAL projectDAL = new ProjectDetails_DAL();
        // GET: ProjectDetails
        public ActionResult Index()
        {
            var projectList = projectDAL.GetAllProjects();
            return View(projectList);
        }

        // GET: ProjectDetails/Details/5
        public ActionResult Details(int id)
        {
            var project = projectDAL.GetProjectById(id);

            if (project == null)
            {
                TempData["InfoMessage"] = "Product not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: ProjectDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectDetails/Create
        [HttpPost]
        public ActionResult Create(ProjectDetails project)
        {
            try
            {
                // TODO: Add insert logic here
                bool isInserted = false;

                if (ModelState.IsValid)
                {
                    isInserted=projectDAL.InsertProject(project);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Project details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the project";
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: ProjectDetails/Edit/5
        public ActionResult Edit(int id)
        {
            var project = projectDAL.GetProjectById(id);
            if (project == null)
            {
                TempData["InfoMessage"] = "Product not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }

                return View(project);

        }

        // POST: ProjectDetails/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProject(ProjectDetails project)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    bool isUpdated = projectDAL.UpdateProject(project);
                    
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Project details updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the project";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(project);
            }
        }

        // GET: ProjectDetails/Delete/5
        public ActionResult Delete(int id)
        {
            var project = projectDAL.GetProjectById(id);
            if (project == null)
            {
                TempData["InfoMessage"] = "Product not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // POST: ProjectDetails/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                bool isDeleted = projectDAL.DeleteProject(id);

                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Project details deleted successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to delete the project";
                }

                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
