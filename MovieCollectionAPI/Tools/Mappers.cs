using DAL = MovieCollectionDAL.Entities;
using WEB = MovieCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    }
}
