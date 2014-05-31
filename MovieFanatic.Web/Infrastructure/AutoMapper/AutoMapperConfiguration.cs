using System.Linq;
using AutoMapper;
using MovieFanatic.Domain;
using MovieFanatic.Web.Controllers;
using StructureMap;

namespace MovieFanatic.Web.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.ConstructServicesUsing(ObjectFactory.GetInstance));

            Mapper.CreateMap<Movie, MovieIndexViewModel.Movie>()
                .ForMember(model => model.Genres, opt => opt.MapFrom(movie => movie.MovieGenres.Select(mg => mg.Genre.Name)))
                .ForMember(model => model.Actors, opt => opt.MapFrom(movie => movie.Characters.Select(ch => ch.Actor.Name)));
        }
    }
}