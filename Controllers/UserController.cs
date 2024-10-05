using Software1_IIPA24.Dtos;
using Software1_IIPA24.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Software1_IIPA24.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserDto user = new UserDto();
            return View(user);
        }

        public ActionResult ListaUsuarios()
        {
            UserService userService = new UserService();
            UserListDto usersList = userService.ListarUsuarios();
            return View(usersList);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserDto newUser)
        {
            try
            {
                UserService userService = new UserService();
                UserDto userResponse = userService.CreateUser(newUser);
                if(userResponse.Response == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(userResponse);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Register
        public ActionResult Login()
        {
            UserDto user = new UserDto();
            return View(user);
        }

        //POST Login
        [HttpPost]
        public ActionResult Login(UserDto user)
        {
            UserService userService = new UserService();
            UserDto userLogin = userService.LoginUser(user);

            if (userLogin.IdUser != 0)
            {
                Session["UserLogged"] = userLogin;
                return RedirectToAction("Dashboard");
            }

            return View(userLogin);
        }

        //GET Dashboard
        public ActionResult Dashboard()
        {
            if (Session["UserLogged"] == null)
            {
                return Redirect("~/User/Login");
            }
            UserDto user = Session["UserLogged"] as UserDto;
            if (user.IdState == 2)
            {
                return RedirectToAction("Inactivo");
            }
            if(user.IdRole == 1)
            {
                return RedirectToAction("ListaUsuarios");
            }
            if (user.IdRole == 2)
            {
                return RedirectToAction("Perfil");
            }       

            return View(user);            
        }

        public ActionResult Logout()
        {
            Session["UserLogged"] = null;
            Session.Abandon();
            return Redirect("~/User");
        }
    }
}
