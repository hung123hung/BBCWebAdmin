using BBCWebAPI.Data;
using BBCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBCWebAPI.Controllers;

namespace BBCWebAPI.Controllers.UI
{
    public class LoginController:Controller
    {
        private DataContext dataContext;
        private User user;
        public LoginController(DataContext dataContext)
        {
            this.dataContext = dataContext;           
        }
        [Route("/")]
        public IActionResult ShowLoginPage()
        {
            return View("Views/Pages/Login.cshtml");
        }
        [HttpPost]
        [Route("/ActionLogin")]
        public IActionResult ActionLogin(string username, string pass)
        {
            user = new User();
            if (user.UserName.Equals(username) && user.PassWord.Equals(pass))
            {
                return Redirect("/HomePage");
            }
            return RedirectToAction(nameof(ShowLoginPage));
        }
    }
}
