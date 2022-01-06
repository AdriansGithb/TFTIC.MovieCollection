using MovieCollectionDAL.Entities;
using System.Collections.Generic;

namespace MovieCollectionDAL.Repositories
{
    public interface IArtistRepository
    {

        #region Artist
        bool Create(Artist a);
        bool Delete(int id);
        IEnumerable<Artist> GetAll();
        Artist GetById(int Id);
        bool Update(int idArtist, Artist a);

        #endregion
        #region Producer
        bool AddProducerToMovie(int idArtist, int idMovie);
        bool DeleteProducer_AllHisMovies(int idArtist);
        bool DeleteProducerOfOneMovie(int idArtist, int idMovie);
        IEnumerable<Artist> GetAllProducers();
        IEnumerable<Artist> GetAllProducersOfOneMovie(int idMovie);

        #endregion
        #region Director
        bool AddDirectorToMovie(int idArtist, int idMovie);
        bool DeleteDirector_AllHisMovies(int idArtist);
        bool DeleteDirectorOfOneMovie(int idArtist, int idMovie);
        IEnumerable<Artist> GetAllDirectors();
        IEnumerable<Artist> GetAllDirectorsOfOneMovie(int idMovie);

        #endregion
        #region Actor
        IEnumerable<Artist> GetAllActors();

        #endregion
    }
}