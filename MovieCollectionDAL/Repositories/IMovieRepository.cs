using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    public interface IMovieRepository
    {
        bool Create(Movie m);
        bool Update(Movie m);

    }
}
