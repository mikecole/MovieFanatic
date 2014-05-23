using System;

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

        private Movie() { }

        public string Title { get; private set; }
        public int ApiId { get; private set; }
        public DateTime ReleaseDate { get; private set; }
    }
}
