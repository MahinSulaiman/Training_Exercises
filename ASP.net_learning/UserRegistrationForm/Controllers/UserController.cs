using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistrationForm.DAL;
using UserRegistrationForm.Models;

namespace UserRegistrationForm.Controllers
{
    public class UserController : Controller
    {
        Users_DAL _usersDAL = new Users_DAL();
        // GET: User
        public ActionResult Index()
        {
            var userList = _usersDAL.GetAllUsers();

            if (userList.Count == 0)
            {
                TempData["ErrorMessage"] = "No users are there";
            }

            return View(userList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _usersDAL.GetUserById(id);
            if (user == null)
            {
                TempData["InfoMessage"] = "User not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(Users user)
        {
            bool isInserted = false;
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    isInserted = _usersDAL.InsertUser(user);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the User";
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _usersDAL.GetUserById(id);
            if (user == null)
            {
                TempData["InfoMessage"] = "User not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Users user)
        {
            bool isUpdated = false;
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    isUpdated = _usersDAL.UpdateUser(user);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "User details updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the user details";
                    }

                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _usersDAL.GetUserById(id);

            if (user == null)
            {
                TempData["InfoMessage"] = "User not available with this Id" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm (int id)
        {
            try
            {
                // TODO: Add delete logic here
                bool isDeleted = _usersDAL.DeleteUser(id);

                if (isDeleted)
                {
                    TempData["SuccessMessage"] = " Successfully deleted the user";
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to delete the user";
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
