using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using Application.SearchObj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGetGenreCommand _getGenres;
        private readonly IGetOneGenreCommand _getOneGenre;
        private readonly ICreateGenreCommand _createGenre;
        private readonly IDeleteGenreCommand _deleteGenre;
        private readonly IUpdateGenreCommand _updateGenre;
        public GenreController(IGetGenreCommand getGenres, IGetOneGenreCommand getOneGenre, ICreateGenreCommand createGenre, IDeleteGenreCommand deleteGenre, IUpdateGenreCommand updateGenre)
        {
            _getGenres = getGenres;
            _getOneGenre = getOneGenre;
            _createGenre = createGenre;
            _deleteGenre = deleteGenre;
            _updateGenre = updateGenre;
        }
        // GET: api/Genre
        [HttpGet]
        public ActionResult<IEnumerable<GenreDto>> Get([FromQuery] GenreSearch obj)
        {
            try
            {
                var data = _getGenres.Execute(obj);
                return Ok(data);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var data = _getOneGenre.Execute(id);
                return Ok(data);
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "Internal server error!");
            }
        }

        // POST: api/Genre
        [HttpPost]
        public IActionResult Post([FromBody] GenreDto obj)
        {
            try
            {
                _createGenre.Execute(obj);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500,"Internal Server Error!");
            }
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] GenreDto obj)
        {
            obj.Id = id;
            try
            {
                _updateGenre.Execute(obj);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch(EntityAlreadyExistException)
            {
                return Conflict();
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _deleteGenre.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
