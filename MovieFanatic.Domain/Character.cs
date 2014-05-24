namespace MovieFanatic.Domain
{
    public class Character : EntityBase
    {
        public Character(string name, Movie movie, Actor actor)
            : this()
        {
            Name = name;
            Movie = movie;
            Actor = actor;
        }
        private Character() { }

        public string Name { get; private set; }

        public virtual Movie Movie { get; private set; }
        public virtual Actor Actor { get; private set; }
    }
}