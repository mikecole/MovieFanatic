using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using MovieFanatic.Data;
using MovieFanatic.Web.Infrastructure;
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
                Movies = _dataContext.Movies.Take(25).Project().To<MovieIndexViewModel.Movie>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var movie = _dataContext.Movies.Find(id);

            if (movie == null)
            {
                return new HttpNotFoundResult("Movie not found.");
            }

            _dataContext.Movies.Remove(movie);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
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
            movies.ForEach(movie => _dataContext.Movies.Add(movie));
            _dataContext.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }

    public class MovieIndexViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } 

        public class Movie
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Overview { get; set; }
            public decimal? AverageRating { get; set; }
            public IEnumerable<string> Genres { get; set; }
            public IEnumerable<string> Actors { get; set; } 
        }
    }
}