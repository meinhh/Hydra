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
    }
}
