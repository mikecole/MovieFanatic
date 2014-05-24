using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MovieFanatic.Domain
{
    public class Movie : EntityBase
    {
        public Movie(string title, int apiId, DateTime releaseDate)
            : this()
        {
            Title = title;
            ApiId = apiId;
            ReleaseDate = releaseDate;
        }
        private Movie()
        {
            MovieGenres = new Collection<MovieGenre>();
            ProductionCompanyMovies = new Collection<ProductionCompanyMovie>();
        }

        public string Title { get; private set; }
        public int ApiId { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string Overview { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; private set; }
        public virtual ICollection<ProductionCompanyMovie> ProductionCompanyMovies { get; private set; }
    }
}
