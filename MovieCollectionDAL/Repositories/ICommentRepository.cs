using MovieCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Repositories
{
    public interface ICommentRepository
    {
        bool Create(Comment c);
        bool Delete(string idUser, int idCom);
        IEnumerable<Comment> GetAll();
        Comment GetById(int Id);
        bool Update(Comment c);

    }
}
