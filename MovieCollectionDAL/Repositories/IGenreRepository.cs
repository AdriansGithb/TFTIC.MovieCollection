using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    interface IGenreRepository
    {
        bool Create(Genre g);
        bool Update(Genre g);

    }
}
