﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using EntityFramework.Filters;
using MovieFanatic.Data;
using MovieFanatic.Domain.Model;
using MovieFanatic.Web.Infrastructure;
using MovieFanatic.Web.Models;
using WebGrease.Css.Extensions;

namespace MovieFanatic.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly DataContext _dataContext;

        public MovieController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ActionResult Index()
        {
            var model = new MovieIndexViewModel
            {
                Movies = _dataContext.Movies
                                     .OrderByDescending(movie => movie.AverageRating)
                                     .Take(25)
                                     .Project().To<MovieIndexViewModel.Movie>()
                                     .ToArray()
            };

            return View(model);
        }

        public ActionResult Deleted()
        {
            _dataContext.DisableFilter("SoftDelete");

            var model = new MovieIndexViewModel
            {
                IsShowingDeleted = true,
                Movies = _dataContext.Movies
                                     .Where(movie => movie.IsDeleted)
                                     .Project().To<MovieIndexViewModel.Movie>()
            };

            _dataContext.EnableFilter("SoftDelete");

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Run a traditional ORM delete. This recognizes interceptors but makes an additional round trip to DB...
            var movie = _dataContext.Movies.Find(id);

            if (movie == null)
            {
                return new HttpNotFoundResult("Movie not found.");
            }

            _dataContext.Movies.Remove(movie);
            _dataContext.SaveChanges();

            //Or batch delete (reduces a round trip to DB). Warning: bypasses interceptors...
            //if (_dataContext.Movies.Where(movie => movie.Id == id).Delete() == 0)
            //{
            //    return new HttpNotFoundResult("Movie not found.");
            //}

            //Or batch update (reduces a round trip to DB) to manage soft deletes. Warning: soft delete logic
            //will be spread throughout your app if you use this method...
            //if (_dataContext.Movies.Where(m => m.Id == id).Update(m => new Movie(m.Title, m.ApiId, m.ReleaseDate) { IsDeleted = true }) == 0)
            //{
            //    return new HttpNotFoundResult("Movie not found.");
            //}

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            _dataContext.DisableFilter("SoftDelete");

            var movie = _dataContext.Movies.Find(id);

            if (movie == null)
            {
                return new HttpNotFoundResult("Movie not found.");
            }

            movie.IsDeleted = false;
            _dataContext.SaveChanges();

            return RedirectToAction("Deleted");
        }

        public ActionResult Refresh()
        {
            var movies = MovieLoader.LoadMovies();

            _dataContext.ProductionCompanyMovies.Delete();
            _dataContext.MovieGenres.Delete();
            _dataContext.Characters.Delete();
            _dataContext.Actors.Delete();
            _dataContext.Movies.Delete();
            _dataContext.Genres.Delete();
            _dataContext.ProductionCompanies.Delete();
            _dataContext.SaveChanges();
            ListExtensions.ForEach(movies, movie => _dataContext.Movies.Add(movie));
            _dataContext.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}