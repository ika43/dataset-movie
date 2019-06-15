using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IUserCommands;
using Application.SearchObj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string serverErrorMessage = "Internal Server Error";
        private readonly IGetOneUserCommand _getOneUser;
        private readonly ICreateUserCommand _createUser;
        private readonly IGetUserCommand _getUser;
        private readonly IDeleteUserCommand _deleteUser;
        private readonly IUpdateUserCommand _updateUser;
        public UserController(IGetOneUserCommand getOneUser, ICreateUserCommand createUser, IGetUserCommand getUser, IDeleteUserCommand deleteUser, IUpdateUserCommand updateUser)
        {
            _getOneUser = getOneUser;
            _createUser = createUser;
            _getUser = getUser;
            _deleteUser = deleteUser;
            _updateUser = updateUser;
        }
        // GET: api/User
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] UserSearch obj)
        {
            try
            {
                var users = _getUser.Execute(obj);
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, serverErrorMessage);
            }
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var user = _getOneUser.Execute(id);
                return Ok(user);
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, serverErrorMessage);
            }
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] UserDto obj)
        {
            try
            {
                _createUser.Execute(obj);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500, serverErrorMessage);
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(string id, [FromBody] UserDto obj)
        {
            obj.Id = id;
            try
            {
                _updateUser.Execute(obj);
                return StatusCode(201);
            }
            catch (EntityNotFoundException)
            {
                return Conflict();
            }
            catch (EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500, serverErrorMessage);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            try
            {
                _deleteUser.Execute(id);
                return NoContent();
            }
            catch(EntityNotFoundException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500, serverErrorMessage);
            }
        }
    }
}
