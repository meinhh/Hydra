using System;
using Hydra.BL;
using Hydra.Data;
using Hydra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Controllers
{
    public class UserController: Controller
    {
        private readonly UserBl _userBl;

        public UserController(HydraContext context)
        {
            _userBl = new UserBl(context);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind("ID", "Name", "Gender")] User user)
        {
            try
            {
                var errorMessage = GetErrorIfInvalid(user);

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    return RedirectToAction("Index", "Error", new { error = errorMessage });
                }

                var userToAdd = new User
                {
                    ID = user.ID,
                    Name = user.Name,
                    Gender = user.Gender
                };

                _userBl.AddUser(user.ID,user.Name,user.Gender);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ID", "Name", "Gender")] User user)
        {
            try
            {
                User userToEdit = _userBl.getById(user.ID);

                if (userToEdit == null)
                {
                    return RedirectToAction("Index", "Error", new { error = string.Format("Could not find product with id {0}", id) });

                }

                var errorMessage = GetErrorIfInvalid(userToEdit);

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    return RedirectToAction("Index", "Error", new { error = errorMessage });
                }

                userToEdit.Name = user.Name;
                userToEdit.Gender = user.Gender;

                _userBl.UpdateUser(userToEdit);
                return View(userToEdit);
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                User userToDelete = _userBl.getById(id);
                try
                {
                    _userBl.DeleteUser(userToDelete);
                }
                catch
                {
                    return RedirectToAction("Index", "Error", new { error = string.Format("Could not find user with id {0}", id) });
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { error = string.Format("Oops! failed to delete product with id {0}", id) });
            }
        }


        private string GetErrorIfInvalid(User user)
        {
            var error = string.Empty;

            if (string.IsNullOrWhiteSpace(user.ID))
            {
                error = "User Id cant be empty or null";
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                error = "user name cant be empty or null";
            }

            return error;
        }

        [HttpGet]
        public void Login(string id, string name)
        {
            _userBl.AddUser(id, name, GetRandom());

            SetUserInSession(id);
        }

        [HttpGet]
        public void Logout()
        {
            SetUserInSession();
        }

        private void SetUserInSession(string id = "")
        {
            HttpContext.Session.SetString("ConnectedUserId", id);
        }

        private Gender GetRandom()
        {
            var num = new Random().Next(1, 4);

            return num == 1 
                ? Gender.Male
                : num == 2 
                    ? Gender.Female 
                    : Gender.Other;
        }

        private bool isValidID(String id)
        {
            if (id.Length != 9) { return false; }

            foreach (char currChar in id)
            {
                if (currChar < '0' || currChar > '9') { return false; }
            }

            return true;
        }

        private bool IsAdminConnected()
        {
            var isAdminConnected = HttpContext.Session.GetInt32("IsAdminConnected") ?? 0;
            return isAdminConnected == 1 ? true : false;
        }
    }
}
