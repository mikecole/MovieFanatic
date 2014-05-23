using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

            var results = JsonConvert.DeserializeObject<RootMovie>(responseContent).results;
            var movies = new List<Domain.Movie>();

            foreach (var result in results)
            {
                request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.themoviedb.org/3/movie/{1}?api_key={0}", apiKey, result.id));
                request.KeepAlive = true;
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentLength = 0;
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }

                var detail = JsonConvert.DeserializeObject<RootMovieDetail>(responseContent);

                movies.Add(new Domain.Movie(detail.title, detail.id, DateTime.Parse(detail.release_date)) { Overview = detail.overview });
            }

            return movies;
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

        private class Genre
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private class ProductionCompany
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        private class ProductionCountry
        {
            public string iso_3166_1 { get; set; }
            public string name { get; set; }
        }

        private class SpokenLanguage
        {
            public string iso_639_1 { get; set; }
            public string name { get; set; }
        }

        private class RootMovieDetail
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public object belongs_to_collection { get; set; }
            public int budget { get; set; }
            public List<Genre> genres { get; set; }
            public string homepage { get; set; }
            public int id { get; set; }
            public string imdb_id { get; set; }
            public string original_title { get; set; }
            public string overview { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public List<ProductionCompany> production_companies { get; set; }
            public List<ProductionCountry> production_countries { get; set; }
            public string release_date { get; set; }
            public long revenue { get; set; }
            public int runtime { get; set; }
            public List<SpokenLanguage> spoken_languages { get; set; }
            public string status { get; set; }
            public string tagline { get; set; }
            public string title { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
        }
    }
}