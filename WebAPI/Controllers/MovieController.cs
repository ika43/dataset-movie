using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IMovieCommands;
using Application.SearchObj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ICreateMovieCommand _createMovie;
        private readonly IGetMovieCommand _getMovie;
        private readonly IGetOneMovieCommand _getOneMovie;
        private readonly IDeleteMovieCommand _deleteMovie;
        private readonly IUpdateMovieCommand _updateMovie;
        public MovieController(ICreateMovieCommand createMovie, IGetMovieCommand getMovie, IGetOneMovieCommand getOneMovie, IDeleteMovieCommand deleteMovie, IUpdateMovieCommand updateMovie)
        {
            _createMovie = createMovie;
            _getMovie = getMovie;
            _getOneMovie = getOneMovie;
            _deleteMovie = deleteMovie;
            _updateMovie = updateMovie;
        }
        // GET: api/Movie
        [HttpGet]
        public IActionResult Get([FromQuery] MovieSearch obj)
        {
            try
            {
                var movies = _getMovie.Execute(obj);
                return Ok(movies);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET: api/Movie/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var movie = _getOneMovie.Execute(id);
                return Ok(movie);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST: api/Movie
        [HttpPost]
        public IActionResult Post([FromBody] MovieDto obj)
        {
            try
            {
                _createMovie.Execute(obj);
                return StatusCode(201);
            }
            catch(EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] MovieDto obj)
        {
            obj.Id = id;
            try
            {
                _updateMovie.Execute(obj);
                return NoContent();
            }
            catch (EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            //catch
            //{
            //    return StatusCode(500);
            //}
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _deleteMovie.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
