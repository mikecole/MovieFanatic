namespace MovieFanatic.Domain
{
    public class Movie : EntityBase
    {
        public Movie(string title)
            : this()
        {
            Title = title;
        }

        private Movie() { }

        public string Title { get; private set; }
    }
}
