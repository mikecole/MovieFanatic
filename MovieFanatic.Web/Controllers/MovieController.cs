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
            var model = _dataContext.Movies.Take(25).Project().To<MovieIndexViewModel>().ToArray();

            return View(model);
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
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        public decimal? AverageRating { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}