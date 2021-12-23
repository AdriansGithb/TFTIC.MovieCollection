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
       bool Create(string name);
       bool Update(Country c);
       bool Delete(int Id);
       IEnumerable<Country> GetAll();
       Country GetById(int Id);

    }
}
