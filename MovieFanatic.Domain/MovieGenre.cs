namespace MovieFanatic.Domain
{
    public class MovieGenre : EntityBase
    {
        public MovieGenre(Movie movie, Genre genre)
            : this()
        {
            Movie = movie;
            Genre = genre;
        }
        private MovieGenre() { }

        public Movie Movie { get; private set; }
        public Genre Genre { get; private set; }
    }
}