using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace MovieFanatic.Web
{
    //This class is ugly, but it's not really the point...
    public class MovieLoader
    {
        public static IEnumerable<Domain.Movie> LoadMovies()
        {
            var result = new List<Domain.Movie>();

            for (var index = 1; index <= 100; index++)
            {
                result.AddRange(LoadMovies(index));
            }

            return result;
        }

        private static IEnumerable<Domain.Movie> LoadMovies(int page)
        {
            var apiKey = ConfigurationManager.AppSettings["tmd-api-key"];

            var request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.themoviedb.org/3/movie/top_rated?api_key={0}&page={1}", apiKey, page));
            request.KeepAlive = true;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentLength = 0;
            string responseContent = null;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }
            }

            return from movie in JsonConvert.DeserializeObject<RootMovie>(responseContent).results
                   where !String.IsNullOrEmpty(movie.release_date)
                   select new Domain.Movie(movie.title, movie.id, DateTime.Parse(movie.release_date));
        }

        //thanks http://json2csharp.com/
        private class Movie
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public int id { get; set; }
            public string original_title { get; set; }
            public string release_date { get; set; }
            public string poster_path { get; set; }
            public double popularity { get; set; }
            public string title { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
        }

        private class RootMovie
        {
            public int page { get; set; }
            public List<Movie> results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }
    }
}