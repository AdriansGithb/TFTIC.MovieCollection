using DAL = MovieCollectionDAL.Entities;
using WEB = MovieCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCollectionDAL.Repositories;

namespace MovieCollectionAPI.Tools
{
    public static class Mappers
    {
        #region Country
        public static WEB.Country toWeb(this DAL.Country c)
        {
            return new WEB.Country
            {
                IdCountry = c.IdCountry,
                Name = c.Name
            };
        }
        public static DAL.Country toDal(this WEB.Country c)
        {
            return new DAL.Country
            {
                IdCountry = c.IdCountry,
                Name = c.Name
            };
        }

        #endregion
        #region Audience
        public static WEB.Audience toWeb(this DAL.Audience a)
        {
            return new WEB.Audience
            {
                IdAudience = a.IdAudience,
                Label = a.Label
            };
        }
        public static DAL.Audience toDal(this WEB.Audience a)
        {
            return new DAL.Audience
            {
                IdAudience = a.IdAudience,
                Label = a.Label
            };
        }

        #endregion
        #region Genre
        public static WEB.Genre toWeb(this DAL.Genre g)
        {
            return new WEB.Genre
            {
                IdGenre = g.IdGenre,
                Label = g.Label
            };
        }
        public static DAL.Genre toDal(this WEB.Genre g)
        {
            return new DAL.Genre
            {
                IdGenre = g.IdGenre,
                Label = g.Label
            };
        }

        #endregion
        #region Movie
        public static WEB.Movie toWeb(this DAL.Movie m, ICountryRepository _cntryRepo = null, IAudienceRepository _audRepo = null, IGenreRepository _gnrRepo = null, IArtistRepository _artRepo = null, IActorRepository _actRepo = null)
        {
            return new WEB.Movie
            {
                IdMovie = m.IdMovie,
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                Synopsys = m.Synopsys,
                TrailerLink = m.TrailerLink,
                IsDeleted = m.IsDeleted,
                OriginCountry = (_cntryRepo != null && m.IdCountry != null) ? _cntryRepo.GetById((int)m.IdCountry).Name : "",
                Audience = (_audRepo != null && m.IdAudience != null) ? _audRepo.GetById((int)m.IdAudience).Label : "",
                Genres = (_gnrRepo != null) ? _gnrRepo.GetByFilmId(m.IdMovie).Select(x=>x.Label) : new List<string>(),
                 Actors = (_actRepo != null) ? _actRepo.GetAllActorsOfOneMovie(m.IdMovie).Select(a=>a.toWeb(_artRepo)) : new List<WEB.Actor>(),
                  Producers = (_artRepo != null) ? _artRepo.GetAllProducersOfOneMovie(m.IdMovie).Select(a=>a.toWeb()) : new List<WEB.Artist>(),
              Directors = (_artRepo != null) ? _artRepo.GetAllDirectorsOfOneMovie(m.IdMovie).Select(a=>a.toWeb()) : new List<WEB.Artist>()
                
            };
        }
        //public static DAL.Movie toDal(this WEB.Movie m)
        //{
        //    return new DAL.Movie
        //    {
        //        IdMovie = m.IdMovie,
        //        Title = m.Title,
        //        ReleaseYear = m.ReleaseYear,
        //        Synopsys = m.Synopsys,
        //        TrailerLink = m.TrailerLink,
        //        IsDeleted = m.IsDeleted,
        //    };
        //}
        public static DAL.Movie toDal(this WEB.MovieForm m)
        {
            return new DAL.Movie
            {
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                Synopsys = m.Synopsys,
                TrailerLink = m.TrailerLink,
                IdCountry = m.OriginCountryId,
                IdAudience = m.IdAudience
            };
        }
        #endregion
        #region Artist
        public static WEB.Artist toWeb(this DAL.Artist a)
        {
            return new WEB.Artist
            {
                IdArtist = a.IdArtist,
                Name = a.Name,
                FirstName = a.FirstName,
                BirthDate = a.BirthDate,
            };
        }
        public static DAL.Artist toDal(this WEB.ArtistForm a)
        {
            return new DAL.Artist
            {
                Name = a.Name,
                FirstName = a.FirstName,
                BirthDate = a.BirthDate
            };
        }
        #endregion
        #region Actor
        public static WEB.Actor toWeb(this DAL.Actor a, IArtistRepository _artRepo = null)
        {
            DAL.Artist artist = _artRepo.GetById(a.IdArtist) ?? new DAL.Artist();
            return new WEB.Actor
            {
                IdArtist = a.IdArtist,
                Name = artist.Name,
                FirstName = artist.FirstName,
                BirthDate = artist.BirthDate,
                IdMovie = a.IdMovie,
                Character = a.Character
                //Characters = new Dictionary<int, List<string>>
                //{
                //    { a.IdMovie, new List<string> {a.Character } }
                //}
            };
        }
        //public static DAL.Artist toDal(this WEB.ArtistForm a)
        //{
        //    return new DAL.Artist
        //    {
        //        Name = a.Name,
        //        FirstName = a.FirstName,
        //        BirthDate = a.BirthDate
        //    };
        //}
        #endregion
        #region AppUser
        public static DAL.AppUser toDal(this WEB.RegisterForm form)
        {
            return new DAL.AppUser
            {
                Email = form.Email,
                Password = form.Password,
                Name = form.Name
            };
        }
        public static WEB.User toWeb(this DAL.AppUser u)
        {
            return new WEB.User
            {
                IdUser = u.IdUser,
                Email = u.Email,
                Name = u.Name,
                IsAdmin = u.IsAdmin,
                Token =""
            };
        }

        #endregion



    }
}
