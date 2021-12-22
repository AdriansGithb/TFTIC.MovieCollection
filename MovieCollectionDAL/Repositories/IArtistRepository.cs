using MovieCollectionDAL.Entities;
using System.Collections.Generic;

namespace MovieCollectionDAL.Repositories
{
    public interface IArtistRepository
    {
        bool Create(Artist a);
        bool Delete(int id);
        IEnumerable<Artist> GetAll();
        Artist GetById(int Id);
        bool Update(Artist a);
    }
}