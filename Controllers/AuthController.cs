using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models.Context;
using MvcTest.Models.Models;
using MvcTest.Repository.Repositories;

namespace MvcTest.Controllers
{
    public class AuthController : Controller
    {
        IAuthInterface authInterface;

        public AuthController(IAuthInterface authInterface)
        {
            this.authInterface = authInterface;
        }
        // GET: Auth

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel userModel)
        {
            ViewBag.error = "";
            int success = authInterface.UserSignUp(userModel);
            if (success == 0)
            {
                ViewBag.error = "Email already in use! please enter another";
                return View();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserEmail, string UserPassword)
        {
            ViewBag.error = "";
            int success = authInterface.UserLogin(UserEmail, UserPassword);
            if (success == 0)
            {
                ViewBag.error = "You have not registered yet! please register";
            }
            else if (success == 2)
            {
                ViewBag.error = "Wrong Password!";
            }
            else if(success==1)
            {
                ViewBag.error = "Login Success!";
                UserModel LogUser = authInterface.GetLoggedUser(UserEmail);
                Session["UserId"] = LogUser.UserId;
                Session["UserName"] = LogUser.UserName;
                Session["UserEmail"] = UserEmail;
                return RedirectToAction("CreateOrder", "Home");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}