using MovieCollectionDAL.Entities;
using System.Collections.Generic;

namespace MovieCollectionDAL.Repositories
{
    public interface IAudienceRepository
    {
        bool Create(string label);
        bool Update(Audience a);
        bool Delete(int Id);
        IEnumerable<Audience> GetAll();
        Audience GetById(int Id);

    }
}