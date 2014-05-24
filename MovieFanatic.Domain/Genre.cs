using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MovieFanatic.Domain
{
    public class Genre : EntityBase
    {
        public Genre(string name)
            : this()
        {
            Name = name;
        }
        private Genre()
        {
            MovieGenres = new Collection<MovieGenre>();
        }

        public string Name { get; private set; }

        public ICollection<MovieGenre> MovieGenres { get; private set; }
    }
}
