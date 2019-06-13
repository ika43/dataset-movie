using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using Application.ICommands.IMovieCommands;
using Application.SearchObj;
using EfCommands.EfMovieCommands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IGetMovieCommand _getMovies;
        private readonly IGetOneMovieCommand _getOneMovies;
        private readonly ICreateMovieCommand _createMovie;
        private readonly IUpdateMovieCommand _updateMovie;
        private readonly IDeleteMovieCommand _deleteMovie;
        private readonly ICreateMovieAndGenreCommand _createMovieAndGenreCommand;

        public MovieController(IGetMovieCommand getMovies, IGetOneMovieCommand getOneMovies, ICreateMovieCommand createMovie, IUpdateMovieCommand updateMovie, IDeleteMovieCommand deleteMovie, ICreateMovieAndGenreCommand createMovieAndGenreCommand)
        {
            _getMovies = getMovies;
            _getOneMovies = getOneMovies;
            _createMovie = createMovie;
            _updateMovie = updateMovie;
            _deleteMovie = deleteMovie;
            _createMovieAndGenreCommand = createMovieAndGenreCommand;
        }

        // GET: Movie
        public ActionResult Index([FromQuery] MovieSearch obj)
        {
            var movies = _getMovies.Execute(obj);
            var modelMovies = movies.Select(p => new MovieViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ImdbRating = p.ImdbRating,
                Plot = p.Plot,
                Runtime = p.Runtime,
                Released = p.Released,
                Genres = p.Genres.Select(g => new GenreViewModel
                {
                    Name = g.Name
                }).ToList()
            });
            return View(modelMovies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(string id)
        {
            var movie = _getOneMovies.Execute(id);
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMovieAndGenreDto obj)
        {
            try
            {
                _createMovieAndGenreCommand.Execute(obj);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityAlreadyExistException)
            {
                TempData["error"] = "Movie with this title already exist!";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server Error, please try later!";
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(string id)
        {
            var movie = _getOneMovies.Execute(id);
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, MovieDto movie)
        {
            try
            {
                movie.Id = id;
                _updateMovie.Execute(movie);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityAlreadyExistException)
            {
                TempData["error"] = "Movie with this name already exist!";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server error, please try later!";
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                _deleteMovie.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}