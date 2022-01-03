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
        public static WEB.Movie toWeb(this DAL.Movie m, ICountryRepository _cntryRepo = null, IAudienceRepository _audRepo = null, IGenreRepository _gnrRepo = null)
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
                Genres = (_gnrRepo != null) ? _gnrRepo.GetByFilmId(m.IdMovie).Select(x=>x.Label) : new List<string>()
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



    }
}
