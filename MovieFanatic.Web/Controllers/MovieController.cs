using System.Linq;
using System.Net;
using System.Web.Mvc;
using EntityFramework.Extensions;
using MovieFanatic.Data;
using WebGrease.Css.Extensions;

namespace MovieFanatic.Web.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult Refresh()
        {
            var movies = MovieLoader.LoadMovies();

            using (var context = new DataContext())
            {
                context.Movies.Delete();
                context.Genres.Delete();
                movies.ForEach(movie => context.Movies.Add(movie));
                context.SaveChanges();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}