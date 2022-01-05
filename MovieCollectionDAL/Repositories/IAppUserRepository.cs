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
        bool Register(AppUser u);
        bool Delete(Guid id);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(int Id);
        bool Update(AppUser u);
        AppUser Login(string email, string pass);
    }
}
