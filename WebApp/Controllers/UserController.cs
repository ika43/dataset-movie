using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IUserCommands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IGetUserCommand _getUser;
        private readonly IGetOneUserCommand _getOneUser;
        private readonly ICreateUserCommand _createUser;
        private readonly IUpdateUserCommand _updateUser;
        private readonly IDeleteUserCommand _deleteUser;

        public UserController(IGetUserCommand getUser, IGetOneUserCommand getOneUser, ICreateUserCommand createUser, IUpdateUserCommand updateUser, IDeleteUserCommand deleteUser)
        {
            _getUser = getUser;
            _getOneUser = getOneUser;
            _createUser = createUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
        }

        // GET: User
        public ActionResult Index()
        {
            try
            {
                return View(_getUser.Execute(new Application.SearchObj.UserSearch { }));
            }
            catch
            {
                TempData["error"] = "Server error!";
                return View();
            }
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                var user = _getOneUser.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "User doesn't exist!";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server Error. Please try later!";
                return View();
            }
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto user)
        {
            try
            {
                _createUser.Execute(user);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistException)
            {
                TempData["error"] = "User Already Exist.";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server Error. Please try later!";
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                var user = _getOneUser.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "User doesn't exist!";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server Error. Please try later!";
                return View();
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, UserDto user)
        {
            user.Id = id;
            try
            {
                _updateUser.Execute(user);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistException)
            {
                TempData["error"] = "User with this username already exist.";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server Error. Please try later!";
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            _deleteUser.Execute(id);
            return RedirectToAction(nameof(Index));
        }
    }
}