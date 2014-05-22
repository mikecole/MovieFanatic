using System.Linq;
using System.Net;
using System.Web.Mvc;
using MovieFanatic.Data;
using MovieFanatic.Domain;
using WebGrease.Css.Extensions;

namespace MovieFanatic.Web.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult Refresh()
        {
            using (var context = new DataContext())
            {
                context.Movies.ToArray().ForEach(movie => context.Movies.Remove(movie));
                context.Movies.Add(new Movie("test"));
                context.SaveChanges();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}