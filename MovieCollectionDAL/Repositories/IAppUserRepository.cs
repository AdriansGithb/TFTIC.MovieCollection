using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    public interface IAppUserRepository
    {
        bool Create(AppUser u);
        bool Delete(int id);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(int Id);
        bool Update(AppUser u);

    }
}
