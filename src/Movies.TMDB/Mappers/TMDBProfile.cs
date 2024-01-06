using AutoMapper;
using Movies.TMDB.Entities;
using Movies.TMDB.Models;
namespace Movies.TMDB.Mappers;
public class TMDBProfile : Profile
{
  public TMDBProfile() 
  {
    CreateMap<TMDBMovie, TMDBMovieEntity>();
  }
}