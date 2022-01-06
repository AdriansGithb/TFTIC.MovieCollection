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
        bool Delete(Guid idUser, int idCom);
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> GetAllUndeletedByMovie(int idMovie);
        Comment GetById(int Id);
        bool Update(Comment c);

    }
}
