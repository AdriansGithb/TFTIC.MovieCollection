using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    public interface IGenreRepository
    {
        bool Create(string label);
        bool Update(Genre g);
        bool Delete(int Id);
        IEnumerable<Genre> GetAll();
        Genre GetById(int Id);
        IEnumerable<Genre> GetByFilmId(int IdFilm);
    }
}
