using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    public interface ICountryRepository
    {
        bool Create(Country c);
        bool Update(Country c);

    }
}
