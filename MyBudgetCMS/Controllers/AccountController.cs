using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBudgetCMS.Controllers
{
    public class AccountController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            //_mapper = mapper;
        }

        // GET: /Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                // Confirm user is not logged in
                string username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                    return RedirectToAction("Index", "Dashboard");

                // return view
                return View();
            }
            catch (Exception ex)
            {
                logger.Error($"Login() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginUserDto model)
        {
            try
            {
                // Check model state
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Check if the user is valid

                bool isvalid = false;
                var users = _userRepository.GetAll().ToList();
                if (users.Any(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)))
                {
                    isvalid = true;
                }

                if (!isvalid)
                {
                    ModelState.AddModelError("CustomError", "שם משתמש וסיסמא אינם נכונים.");
                    return View(model);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Login() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //// GET: /account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: /account/Register
        [HttpPost]
        public ActionResult Register(UserDto model)
        {
            try
            {
                // Check model state
                if (!ModelState.IsValid)
                {
                    return View("CreateAccount", model);
                }

                // Check if passwords match
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View("CreateAccount", model);
                }

                // Make sure username is unique
                if (_userRepository.GetAll().Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", $"username {model.Username} is taken.");
                    model.Username = "";
                    return View("CreateAccount", model);
                }

                // Create userDTO
                User userDTO = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password
                };

                // Add the DTO
                _userRepository.Add(userDTO);

                // Add to UserRolesDTO
                int id = userDTO.UserId;

                UserRole userRoleDTO = new UserRole()
                {
                    UserId = id,
                    RoleId = 2
                };

                _userRepository.AddUserRole(userRoleDTO);

                // Redirect
                return View("Login");
            }
            catch (Exception ex)
            {
                logger.Error($"CreateAccount() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        // GET: /Account/Logout
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }

        [Authorize]
        public ActionResult UserNavPartial()
        {
            try
            {
                // Get username
                string username = User.Identity.Name;

                // Declare model
                UserNavPartialDto model;

                // Get the user
                User dto = _userRepository.Get(username);

                // Build the model
                model = new UserNavPartialDto()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                // Return partial view with model
                return PartialView(model);
            }
            catch (Exception ex)
            {
                logger.Error($"UserNavPartial() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET: /Account/user-prifile
        [HttpGet]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile()
        {
            try
            {
                // Get username
                string username = User.Identity.Name;

                // declare model
                UserProfileDto model;

                // get user
                User dto = _userRepository.Get(username);

                // build model
                model = Mapper.Map<User, UserProfileDto>(dto);

                // Return view with model
                return View("UserProfile", model);
            }
            catch (Exception ex)
            {
                logger.Error($"UserProfile() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //POST: /Account/user-prifile
        [HttpPost]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile(UserProfileDto model)
        {
            try
            {
                // Check model state
                if (!ModelState.IsValid)
                {
                    return View("UserProfile", model);
                }

                // Check if passwords match if need be
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    if (!model.Password.Equals(model.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Passwords do not match.");
                        return View("UserProfile", model);
                    }
                }

                // Get usernamez
                string username = User.Identity.Name;

                // make sure username is unique
                if (_userRepository.GetAll().Where(x => x.UserId != model.Id).Any(x => x.Username == username))
                {
                    ModelState.AddModelError("", $"username {model.Username} already exsists.");
                    model.Username = "";
                    return View("UserProfile", model);
                }

                // Edit DTO
                User dto = _userRepository.Get(model.Id);

                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
                dto.Email = dto.Email;
                dto.Username = model.Username;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    dto.Password = model.Password;
                }

                // Return view with model
                return Redirect("~/account/User-profile");
            }
            catch (Exception ex)
            {
                logger.Error($"UserProfile() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }
    }
}